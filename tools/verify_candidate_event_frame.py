#!/usr/bin/env python3
"""Independent standard-library check of the Candidate V1 reference frame.

This intentionally does not call the C# codec. It independently recomputes the
SimulationEventId V1 hash from the documented identity fields, assembles the
candidate journal frame, and verifies the complete reference bytes, field
boundaries, and checksum.
"""
from hashlib import sha256
from struct import pack
import unittest

WORLD_SEED = 123
MODEL = b"v1"
ADDRESS = bytes.fromhex("5257534101002A00000002030405090001")
DOMAIN = b"resource.depleted"
ORDINAL = 7
LOGICAL_TIME = b"tick:000042"
PAYLOAD = bytes.fromhex("102030")
EVENT_ID = bytes.fromhex(
    "F223794D5176C58304EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1"
)
EXPECTED_FRAME = bytes.fromhex(
    "5257454A0100000000A0000000000000007B000000027631000000115257534101002A"
    "00000002030405090001000000117265736F757263652E6465706C6574656400000000"
    "000000070000000B7469636B3A30303030343200000003102030F223794D5176C58304"
    "EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1BCE9CCE3807A763076F7561"
    "57C1D83F117AFFBFBC6831B3427F876FC901D6567"
)


def field(value: bytes) -> bytes:
    return pack(">I", len(value)) + value


def build_event_id() -> bytes:
    identity = (
        b"RWEI"
        + bytes((1,))
        + pack(">Q", WORLD_SEED)
        + field(MODEL)
        + field(ADDRESS)
        + field(DOMAIN)
        + pack(">Q", ORDINAL)
        + field(LOGICAL_TIME)
    )
    return sha256(identity).digest()


def build_frame() -> bytes:
    body = (
        b"RWEJ"
        + bytes((1, 0))
        + pack(">I", 160)
        + pack(">Q", WORLD_SEED)
        + field(MODEL)
        + field(ADDRESS)
        + field(DOMAIN)
        + pack(">Q", ORDINAL)
        + field(LOGICAL_TIME)
        + field(PAYLOAD)
        + EVENT_ID
    )
    return body + sha256(body).digest()


class CandidateFrameReferenceTests(unittest.TestCase):
    def test_event_identity_is_independently_recomputed(self):
        self.assertEqual(EVENT_ID, build_event_id())

    def test_independent_assembly_matches_documented_full_vector(self):
        frame = build_frame()
        self.assertEqual(160, len(frame))
        self.assertEqual(EXPECTED_FRAME, frame)
        self.assertEqual(
            "BCE9CCE3807A763076F756157C1D83F117AFFBFBC6831B3427F876FC901D6567",
            frame[-32:].hex().upper(),
        )

    def test_fixed_offsets_and_boundaries(self):
        frame = build_frame()
        self.assertEqual(b"RWEJ", frame[0:4])
        self.assertEqual((1, 0), (frame[4], frame[5]))
        self.assertEqual(160, int.from_bytes(frame[6:10], "big"))
        self.assertEqual(WORLD_SEED, int.from_bytes(frame[10:18], "big"))
        self.assertEqual(2, int.from_bytes(frame[18:22], "big"))
        self.assertEqual(MODEL, frame[22:24])
        self.assertEqual(17, int.from_bytes(frame[24:28], "big"))
        self.assertEqual(ADDRESS, frame[28:45])
        self.assertEqual(17, int.from_bytes(frame[45:49], "big"))
        self.assertEqual(DOMAIN, frame[49:66])
        self.assertEqual(ORDINAL, int.from_bytes(frame[66:74], "big"))
        self.assertEqual(11, int.from_bytes(frame[74:78], "big"))
        self.assertEqual(LOGICAL_TIME, frame[78:89])
        self.assertEqual(3, int.from_bytes(frame[89:93], "big"))
        self.assertEqual(PAYLOAD, frame[93:96])
        self.assertEqual(EVENT_ID, frame[96:128])
        self.assertEqual(sha256(frame[:128]).digest(), frame[128:160])


if __name__ == "__main__":
    unittest.main(verbosity=2)
