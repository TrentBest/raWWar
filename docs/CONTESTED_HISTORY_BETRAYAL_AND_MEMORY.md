# raWWar — Contested History, Betrayal, and the Politics of Memory

> **The victor may control the archive, the classroom, the monument, and the ceremony. That does not mean the victor controls what happened.**

Status: Candidate historical-evidence design. This extends [Humanity's Long History and the Discoverable Past](HUMANITYS_LONG_HISTORY_AND_DISCOVERABLE_PAST.md) and the [Campaign History Discoveries contract](../data/campaign-history-discoveries.json). It does not establish new named events or factions as canon.

## 1. History is not the same thing as the official account

A persistent universe needs one underlying sequence of events for simulation continuity, but people do not share one neutral view of that sequence. They experience different parts of events, preserve different records, remember imperfectly, protect themselves, inherit grievances, and live under institutions with unequal power.

A victorious government may seize enemy archives, destroy embarrassing orders, publish selected casualty totals, rename a massacre a security operation, or teach generations that a failed invasion was a glorious strategic withdrawal. A defeated government may preserve evidence the victor tried to erase—or manufacture a martyr story to conceal its own crimes. Neither side is automatically truthful.

**Power changes which account is repeated and preserved. It does not automatically make that account true.**

## 2. Keep five things separate

| Layer | What it means | Example |
|---|---|---|
| **Event** | What the simulation says occurred, including causation and consequences | A relief fleet was ordered to hold position while a colony was attacked |
| **Account** | A source's claims about the event | The admiral says communications were lost |
| **Evidence** | Records and physical traces that support, constrain, or contradict claims | The order exists; the relay log shows a functioning link; the ship's recorder has a gap |
| **Interpretation** | A reasoned explanation of what the evidence means | The admiral may have received the order but chosen not to act |
| **Public narrative** | The account an institution promotes or enforces | The official history calls the delay unavoidable and celebrates the admiral |

The player's understanding is a sixth, separate layer. The player may have only a rumor, a convincing but forged document, or three authentic records that still do not prove intent.

Do not flatten these layers into one lore string or a single `isTrue` flag.

## 3. Betrayal should be causal, relational, and consequential

Betrayal means a consequential breach of an established relationship, duty, expectation, or agreement. It is not merely an NPC revealing that they were secretly evil.

A betrayal record needs to establish:
- **The relationship or obligation:** command trust, an alliance, a treaty, crew duty, a family succession promise, an intelligence arrangement, a corporate contract, civilian protection, a revolutionary pledge, or a critical logistics dependency.
- **The expected action and actual action:** what someone was relied upon to do, and what they did instead.
- **Knowledge and alternatives:** what the actor knew at the time, what choices were available, and whether coercion, misinformation, or damaged communications constrained the choice.
- **Motive as a claim:** stated motives and inferred motives are not interchangeable with objective facts.
- **Consequences:** casualties, broken trust, mutiny, succession disputes, altered alliances, new procedures, investigations, and choices that persist after the original actor is gone.
- **Evidence and accounts:** what the affected party believes, what the actor claims, what the institution publishes, and what can actually be corroborated.

One action can honor one obligation while betraying another. An officer who disobeys an unlawful order might be called a traitor by the state, a savior by civilians, and a dangerous precedent by other officers. The simulation should preserve those perspectives rather than forcing one universal reputation label.

## 4. Battles acquire competing histories

A battle is one persistent event, not a separate event for every faction's story. Its accounts may disagree about:

- who fired first, and whether that first shot was authorized;
- whether an order existed, was received, was understood, or was obeyed;
- whether a withdrawal was planned, forced, or a rout;
- who abandoned whom, and what each commander knew at the time;
- civilian presence and casualties;
- whether surrender terms were offered or honored;
- whether a loss was an accident, negligence, sacrifice, or deliberate action;
- who achieved the objective—and whether the claimed objective was the real one.

Those differences should arise from perspective, access, memory, incentives, missing evidence, propaganda, and deception. They should not be generated as arbitrary flavor text.

A useful example, deliberately **illustrative and not canon**:

1. The official victory chronicle says the garrison held until its ammunition was exhausted.
2. A survivor says the commander ordered a withdrawal and abandoned a civilian convoy.
3. The tactical recorder shows that the convoy's transponder was still active when the garrison departed.
4. A logistics manifest suggests ammunition remained aboard a sealed reserve vehicle, but its custody record is broken.
5. A private letter indicates the commander feared the convoy was carrying a contagious threat; it does not prove that the fear was justified.
6. A later inquiry suppresses the letter and praises the commander.
7. The player may establish that the official account omitted the convoy—but still be unable to prove the commander's intent.

That last distinction matters. Evidence can overturn a public story without answering every question.

## 5. How victors shape memory

The data contract models concrete mechanisms, not an all-powerful abstract “propaganda” stat:

- archive seizure, destruction, classification, and selective release;
- school curricula, memorials, ceremonies, calendars, and renaming;
- witness coercion, rewards, social pressure, and threats against families;
- genuine documents published without their surrounding context;
- forged orders, staged evidence, false flags, and impersonation;
- command cover-ups protecting careers, ministries, companies, or dynasties;
- defeated-side mythmaking and self-exculpation;
- translation drift, incompatible dates, damaged recordings, and changing terminology;
- sincere institutional self-deception, where people repeat the story that preserves their identity.

These mechanisms need actors, capabilities, opportunities, costs, traces, and counter-evidence. A faction cannot erase a record it never controlled; a forgery needs a plausible route into custody; a cover-up may leave discrepancies in maintenance, staffing, fuel, casualty, or communications records.

And the attempt to conceal history can itself become history.

## 6. Evidence has provenance and failure modes

A recovered record proves less than players may first think. An authentic order proves that the order was issued—not that it was received, obeyed, lawful, or the true cause of an outcome. A sensor record may be reliable about motion and poor at identifying intent. Several chronicles may all repeat one original report, creating the illusion of independent corroboration.

Every claim should retain:
- source and custody chain;
- source independence and shared dependencies;
- when it was created versus the time it describes;
- integrity, authenticity, translation, and reconstruction status;
- supporting and contradicting evidence;
- omitted topics and known blind spots;
- confidence at the **claim** level, not just one confidence number for an entire document;
- revisions, including who changed the account and why when that is known.

Absence can matter, but only when the record would reasonably be expected to exist and its survival conditions are understood. Missing evidence is not automatic proof of a conspiracy.

## 7. Discovery changes the world through belief and action

Players can preserve, copy, authenticate, cross-reference, interview, translate, publish, leak, suppress, trade, alter, or destroy evidence—where their physical access and authority permit it.

Consequences flow through what people learn and believe:
- a treaty claim becomes contestable;
- a faction loses trust in a commander;
- a mutiny is prevented—or triggered by a misleading leak;
- an old enemy becomes a necessary ally;
- a memorial is challenged;
- a mission changes because an officer's stated motive is exposed;
- a government retaliates against witnesses or quietly revises its schoolbooks;
- a player publishes evidence that later generations reinterpret.

Discovery does **not** automatically rewrite the event. If the player proves an atrocity occurred, that does not resurrect its victims; if the evidence is suppressed, the event still happened even if public knowledge never catches up. World truth, actor belief, public narrative, and player knowledge remain distinct.

## 8. Make the archive an investigative system, not a lore dump

This contract extends the existing record model with account kinds, claim references, source dependencies, provenance chains, and claim-level confidence. Original records remain available; summaries must preserve links back to them. Contradictions are first-class relationships, not errors to be silently reconciled.

The player should be able to move from physical evidence to operational reconstruction, historical context, contested interpretation, and deeper archives. They should not need to read every record to play. Search and investigation can follow people, units, systems, locations, dates, events, materials, obligations, and political claims.

## 9. Validation and canon discipline

The machine-readable contract is [`contested-history-and-betrayal.json`](../data/contested-history-and-betrayal.json). It establishes design requirements, not final plot canon.

Tests should ensure that:
- official victory accounts receive no automatic truth bonus;
- defeated accounts receive no automatic truth bonus either;
- one event can support several non-identical accounts;
- copied sources are not mistaken for independent corroboration;
- betrayal references an actual relationship or obligation and carries consequences;
- evidence provenance survives summaries and reconstruction;
- the player's knowledge changes only through permitted discovery or communication;
- political outcomes depend on what relevant actors believe as well as what occurred;
- contradictions and uncertainty remain inspectable;
- illustrative incidents are never silently promoted into canon.

The governing rule is simple: **the galaxy remembers what happened; civilizations argue over what it meant; power decides which arguments are easiest to hear.**