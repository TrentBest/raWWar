using System.Buffers.Binary;

namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Experimental, single-process append-only event journal. Internal and provisional: this
/// is not a public storage API, a multi-process store, or an atomic world-state transaction.
/// </summary>
internal sealed class CandidateFileEventJournal : IDisposable
{
    private readonly object _gate = new();
    private readonly FileStream _stream;
    private readonly Action? _beforeDurableFlush;
    private readonly Dictionary<SimulationEventId, SimulationEvent> _events = new();
    private bool _requiresReopen;
    private bool _disposed;

    internal CandidateFileEventJournal(string path, Action? beforeDurableFlush = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        _beforeDurableFlush = beforeDurableFlush;
        var fullPath = Path.GetFullPath(path);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        _stream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
            FileShare.None, 4096, FileOptions.WriteThrough);
        try
        {
            Recover();
            _stream.Position = _stream.Length;
        }
        catch
        {
            _stream.Dispose();
            throw;
        }
    }

    internal int Count
    {
        get { lock (_gate) { EnsureUsable(); return _events.Count; } }
    }

    internal EventCommitResult Commit(SimulationEvent candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate);
        lock (_gate)
        {
            EnsureUsable();
            if (_events.TryGetValue(candidate.Id, out var existing))
                return existing.HasSameContent(candidate)
                    ? EventCommitResult.AlreadyCommitted
                    : EventCommitResult.IdentityConflict;

            var frame = CandidateEventFrameCodec.Encode(candidate);
            try
            {
                _stream.Position = _stream.Length;
                _stream.Write(frame);
                // Internal fault seam for testing the uncertain outcome between append and flush.
                _beforeDurableFlush?.Invoke();
                _stream.Flush(flushToDisk: true);
                _events.Add(candidate.Id, candidate);
                return EventCommitResult.Committed;
            }
            catch
            {
                // A write/flush failure can leave a complete, partial, or durable record.
                // Do not accept more work against a potentially stale index; reopen and
                // run full recovery to resolve the outcome.
                _requiresReopen = true;
                throw;
            }
        }
    }

    internal IReadOnlyList<SimulationEvent> ReadOrdered(string eventDomain, ReadOnlySpan<byte> canonicalAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventDomain);
        if (canonicalAddress.IsEmpty)
            throw new ArgumentException("A canonical entity or region address is required.", nameof(canonicalAddress));

        var streamKey = Convert.ToHexString(canonicalAddress);
        lock (_gate)
        {
            EnsureUsable();
            return _events.Values
                .Where(e => StringComparer.Ordinal.Equals(e.EventDomain, eventDomain)
                    && StringComparer.Ordinal.Equals(e.StreamKey, streamKey))
                .OrderBy(e => e.LogicalTimeKey, StringComparer.Ordinal)
                .ThenBy(e => e.EventOrdinal)
                .ThenBy(e => e.Id.Value, StringComparer.Ordinal)
                .ToArray();
        }
    }

    private void Recover()
    {
        _stream.Position = 0;
        var header = new byte[CandidateEventFrameCodec.HeaderLength];
        while (_stream.Position < _stream.Length)
        {
            var recordStart = _stream.Position;
            ReadExactly(header, "truncated event-frame header");
            if (!header.AsSpan(0, 4).SequenceEqual("RWEJ"u8))
                throw new FormatException($"Invalid event-frame magic at offset {recordStart}.");
            if (header[4] != 1)
                throw new FormatException($"Unsupported event-frame version at offset {recordStart}.");
            if (header[5] != 0)
                throw new FormatException($"Unsupported event-frame flags at offset {recordStart}.");

            var length = BinaryPrimitives.ReadUInt32BigEndian(header.AsSpan(6, 4));
            if (length < CandidateEventFrameCodec.HeaderLength + CandidateEventFrameCodec.DigestLength
                || length > CandidateEventFrameCodec.MaxFrameBytes)
                throw new FormatException($"Invalid event-frame length at offset {recordStart}.");
            var frame = new byte[checked((int)length)];
            header.CopyTo(frame, 0);
            ReadExactly(frame.AsSpan(header.Length), "truncated event frame");
            var simulationEvent = CandidateEventFrameCodec.Decode(frame);
            if (!_events.TryAdd(simulationEvent.Id, simulationEvent))
                throw new FormatException($"Duplicate event identity in journal at offset {recordStart}.");
        }
    }

    private void ReadExactly(Span<byte> destination, string error)
    {
        var offset = 0;
        while (offset < destination.Length)
        {
            var read = _stream.Read(destination[offset..]);
            if (read == 0)
                throw new FormatException(error + ".");
            offset += read;
        }
    }

    private void EnsureUsable()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (_requiresReopen)
            throw new InvalidOperationException("A previous journal write had an uncertain outcome; dispose and reopen to recover before continuing.");
    }

    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed) return;
            _disposed = true;
            _stream.Dispose();
        }
    }
}
