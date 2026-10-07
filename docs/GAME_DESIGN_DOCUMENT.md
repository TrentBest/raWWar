# raWWar — Game Design Document

Status: Living production document.

## Purpose
This is the working Game Design Document (GDD) for raWWar. It turns the Game Design Bible into a production-oriented description of the Experience: what the player does, what systems support it, what modes exist, and what still needs to be designed.

The GDD does not override the Game Design Bible. Where the two differ, the Bible is authoritative unless the creator explicitly changes the design.

## Design authority
- **Canon:** established creator decisions.
- **Candidate:** proposed design awaiting confirmation.
- **Experiment:** a prototype or test intended to discover whether an idea works.
- **Open question:** intentionally unresolved.

## High concept
raWWar is a first-person science-fiction war Experience in which being a person inside a living military organization is the game.

The soldier is the star. Combat is one occupation among many. Training, qualification, maintenance, research, construction, logistics, piloting, command, intelligence, recreation, and social life all belong to the same world.

## Core player loop
The exact loop varies by role, but the common pattern is:
1. Exist in the organization.
2. Observe what is happening.
3. Receive or discover opportunities, duties, orders, and needs.
4. Qualify and prepare.
5. Perform the work physically in the world.
6. Experience consequences.
7. Earn capability, identity, reputation, equipment, rank, or organizational change.
8. Decide what to pursue next.

## Game modes
1. Campaign — Soldier
2. Campaign — Officer
3. Campaign — Cooperative
4. Multiplayer — Free For All
5. Multiplayer — Team vs Team
6. Multiplayer — Persistent
7. Massively Online — Fully Persistent Universes

Cooperative campaign is intended to scale beyond conventional small-party multiplayer. A session may eventually support anything from two players to approximately 128 cooperating participants.

"Cooperative" does not necessarily mean every participant belongs to the same side. Players may participate in the same campaign while occupying different factions, objectives, or relationships.

## Player identity
A player can begin in a campaign as a Commander as established by the current opening design, while other modes may establish different starting contexts.

Long-term identity is expected to include role, qualifications, rank, awards, equipment, possessions, reputation, relationships, and history.

The exact persistence boundary between modes remains open.

## Equipment and customization
The visual and mechanical identity of raWWar will be developed around an original science-fiction military design language.

The foundational soldier concept is an exoskeleton-based military system. A relatively accessible entry-level exoskeleton can become the base for increasingly advanced additions and customizations.

Candidate equipment dimensions include:
- chassis/exoskeleton;
- armor;
- power;
- mobility;
- sensors;
- communications;
- tools;
- weapons;
- specialist modules;
- decorations;
- personal modifications.

Equipment should be meaningful rather than purely cosmetic. It may impose qualifications, maintenance, power, training, compatibility, readiness, or operational requirements.

Persistent-universe customization is a candidate area for substantially greater freedom than campaign/cooperative soldier play.

## Gestures and embodied motion

raWWar should not treat soldiers as identical animated meshes. Their physical motion is generated from shared ideal procedures and individualized execution.

A **Gesture** is a motion flipbook: a sequence of poses plus the mathematical rule describing how to progress from one pose toward the next. The ideal Gesture defines what the action is; the individual soldier's seed, statistics, state, and circumstances create that soldier's fuzzy realization of the action.

This allows ten thousand soldiers to perform the same march, maintenance task, boarding procedure, or formation movement without producing ten thousand mechanically synchronized copies. Small bounded differences can include speed, lateral offset, stride timing, head direction, reaction timing, posture, recovery, and occasional stumbles or corrections.

Gestures are also how raWWar connects generic soldiers to specialized world objects. A building, vehicle, machine, or other capability can expose a **Gesture Provider**. A soldier can query for the procedure it needs — for example, entering a cockpit — move to the appropriate interaction point, and then have the provided FSM govern the physical procedure.

This means the soldier does not need hard-coded knowledge of every vehicle or building. The object that owns the physical interface provides the procedure.

## World life
The world should always be doing something.

Observed locations should provide evidence of living systems: soldiers training, formations moving, crews servicing vehicles, researchers working, logistics moving materials, construction progressing, command staff monitoring operations, and personnel talking about both important and completely mundane matters.

Background conversations may reveal lore, rumors, jokes, complaints, relationships, and ordinary life. They do not all exist to explain the plot.

## First-person rule
The player is physically present in the world. Menus, management, research, construction, recreation, command, and combat should be diegetic wherever practical.

This is an immersion rule, not a VR-only rule.

## Campaign opening
The campaign begins with the moniker, lightning, the approximately eighty-floor view across a massive rain-soaked square toward an obscene Imperial palace, observation of the living exterior, and the arrival of two Imperial soldiers ordering the Commander to report to the Empress.

The opening teaches through consequence. Exploring the faction headquarters is possible, but refusing the immediate order results in execution after a short warning.

The next sequence after obedience remains an open design question.

## Death
Death means immediate loss of the current attempt. The player should not treat death as an ordinary checkpoint reset.

Campaign checkpoints may reduce unnecessary repetition, but do not change the meaning of death.

## Progression
Training and qualification are primary progression systems.

Qualifications open occupations, equipment, facilities, and responsibilities. Rank and status may gate some paths. Progression should be visible physically through insignia, ribbons, equipment, and behavior.

The exact qualification graph remains to be designed.

## Scale
The world is intended to support enormous military organizations, including potentially hundreds of thousands of personnel and hundreds of scientists and support staff.

Not every entity must run at maximum simulation fidelity at all times. The production problem is to preserve a convincing living system while using appropriate levels of simulation and observation-relative detail. The Renderer can progressively reduce pose detail, Gesture evaluation, and visual computation with distance through event horizons while retaining the properties that make the population look alive.

## Technology as game design

raWWar is intentionally designed around the Workshop architecture rather than treating that architecture as an implementation detail.

- **FSM_API** supplies the state-machine behavior that lets individual entities follow complex, interdependent procedures.
- **FSM_COS** composes the capabilities required by an Experience without becoming the game itself.
- **MicroBundles** allow buildings, vehicles, occupations, equipment, and other capabilities to expose their own meaningful services, including Gesture Providers.
- **AnyApp** is the primary host and heavy-processing manifestation of the Experience. It can host the same manifest without defining the game.
- **The Workshop Renderer** converts authoritative state into an observation-relative presentation. Event horizons allow computation to be concentrated where the player can actually perceive detail.
- **GPU compute** can operate on compact state data across large populations rather than requiring a conventional CPU animation object for every soldier.

The intended result is a game in which enormous numbers of soldiers can remain individually plausible because the data and relationships describe what they need to do, while FSMs and the Renderer determine how that behavior becomes visible.

The technology is therefore not merely supporting raWWar. The game is a demonstration of why the technology exists.

## Design questions
The GDD is intentionally incomplete. Unresolved subjects are tracked in OPEN_QUESTIONS.md and future system-specific documents.

## Non-goals
- A conventional class-based shooter.
- A VR-only game definition.
- Static NPC decoration.
- A collection of disconnected mini-games.
- Hard-coded behavior for every occupation.
- Treating rendering technology as the definition of the Experience.
