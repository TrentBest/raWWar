# raWWar — Game Design Bible

Status: Living design document.

Authority: creator statements are canon unless explicitly marked otherwise.

## 1. What raWWar is

raWWar is a fully first-person, immersive war Experience.

It is not fundamentally a shooter with unrelated extra modes. It is a living war in which the player inhabits a role inside the war, and that role can be radically different from player to player.

The central design statement is:

> **The soldiers themselves are the stars of this game.**

Commanders, researchers, mechanics, builders, pilots, operators, crew, scientists, and other roles exist because they are ways for players to participate in the same world.

## 2. Platform and immersion model

raWWar is **AnyApp first**.

The primary runtime is a desktop AnyApp host. The Experience is designed and simulated so that the desktop runtime is the authoritative companion capable of performing the heavy processing required by the world.

**MyVR comes next.** A VR client can connect to the AnyApp companion and receive the information required to present the immersive first-person view. The VR device performs the work that is more efficient or necessarily local to the headset, while substantial simulation and processing can remain on the desktop companion.

This establishes a deliberate split:

- **AnyApp** — primary Experience host and heavy-processing companion.
- **MyVR** — immersive VR client and device-side presentation/runtime boundary.
- **raWWar** — the Experience and game simulation, not a VR application itself.
- **Renderer** — provides the rendering technology and platform-neutral rendering boundary used by the Experience.

The architecture must not make VR a prerequisite for the game.

The game itself, however, is always **FPS and immersive**. The player should experience the world from first-person presence whether that presentation is delivered to a desktop display or through a VR client.

## 3. Modes of participation

The current intended modes are:

1. Campaign — Soldier
2. Campaign — Officer
3. Campaign — Cooperative
4. Multiplayer — Free For All
5. Multiplayer — Team vs Team
6. Multiplayer — Persistent
7. Massively Online — Fully Persistent Universes

These should eventually share the same underlying world model rather than becoming unrelated implementations.

## 4. First-person rule

raWWar is always experienced from the first-person perspective.

The player is physically present in the world.

Menus, management, recreation, research, construction, command, and combat should therefore be designed as diegetic activities wherever practical.

This is an **experience rule**, not a hardware rule: first-person immersion does not imply VR-only.

## 5. Soldier life

A soldier can qualify for equipment, accept missions, deploy with a squad, remain with their unit when no mission is available, enter downtime, train in barracks, participate in specialized squads, operate specialized vehicles and equipment, and pursue activities outside direct combat.

Downtime is not dead time.

The intended world contains places such as an arcade and simulated air hockey so players can socialize and play games while remaining inside the world.

## 6. Command life

Command is also first-person.

A commander occupies a command center and interacts with staff and advisors.

Command activities include overseeing forces, talking directly with researchers, requesting research, scheduling demonstrations, designing bases, deciding facility capacity, commissioning military reviews, delivering speeches, joining battles personally, selecting bodyguards, and making consequential decisions after major engagements.

A commander is not merely a detached strategy screen. The commander is a person inside the command organization.

## 7. Military reviews and leadership

A commander may pay for military reviews.

A review can create an opportunity for speeches, troop motivation, ceremonial presence, and direct interaction with forces.

A well-delivered speech may materially motivate troops and enable extraordinary performance.

The exact mechanics of morale, speech quality, audience response, and leadership bonuses remain to be designed.

## 8. Headquarters assault

One intended high-level command scenario is an assault on an enemy headquarters.

A commander who participates personally may infiltrate the enemy base, reach the enemy headquarters, breach and clear it, reach the blast doors separating the enemy commander from the assaulting force, breach those doors, fight the close-quarters battle, encounter the opposing commander, communicate directly with the defeated opponent, and decide whether the opponent is executed, imprisoned, or released.

The dramatic confrontation is intended to preserve player agency rather than resolve the moment through a cutscene.

## 9. Organization, staffing, and living military scale

The military base is science-fiction military infrastructure, not a small collection of generic buildings.

A base can contain aircraft, launch pads, ground crews, scientists, maintenance personnel, security, logistics personnel, and large numbers of trained soldiers. A facility may be physically present but operationally unavailable when it does not have enough qualified personnel to staff it.

For example, an aircraft can require a flight crew plus a trained ground crew. A VTOL launch pad can require an eight-person crew per shift. If the organization cannot supply enough qualified people, the launch pad has inoperable periods rather than magically functioning anyway.

The same principle applies throughout the military: **capacity is not the same thing as readiness**. Facilities, vehicles, squads, and formations become operational through people and qualifications.

Barracks remain a foundational training structure. A barracks supports a squad of four, while advanced facilities and vehicles can require multiple specialized squads. The resulting staffing requirements are intended to create a living organization rather than an abstract unit-count economy.

The intended visual scale is enormous. A commander looking across a major base may see hundreds of thousands of soldiers and staff moving with military precision, alongside hundreds of scientists and the personnel required to maintain the facility. From command, these can be represented as numbers, requisition goals, readiness targets, and staffing requirements. To the individual soldier, the same numbers represent jobs, advancement, training opportunities, and people they know.


Soldiers train in barracks.

A barracks supports a squad of four.

Advanced facilities and vehicles can require multiple squads.

When multiple specialized squads are required, each squad has a distinct training role and therefore requires appropriate barracks capacity.

This makes base design part of military organization. A commander is not merely placing decorative buildings; the base determines what the organization can train, maintain, and operate.

## 10. Qualification, identity, and progression

Training is a primary pillar of raWWar.

Soldiers are not generic interchangeable combat units. A soldier can train, qualify, earn ratings, gain rank, receive insignia, and accumulate visible recognition for what they have learned to do.

The intended presentation includes a physical ribbon or other award displayed on the player's chest for each qualification or achievement, together with ratings and rank insignia. The player's history should therefore become visible on the body of the soldier.

Qualifications are intended to open opportunities. A soldier may pursue different training paths depending on what they want to do, including specialist equipment, research, construction, piloting, command, intelligence, sabotage, or other military occupations.

Some paths can require a change in status. For example, flight training may require conversion to officer status. Officer training can include an immersive simulation program that deliberately exposes the trainee to demanding situations, interrupts expectations, and tests their ability to cope with command pressure. These simulations are not necessarily intended to teach the final skill directly; they can be designed to shock, challenge, and reveal what the trainee is capable of, followed by recovery and a choice of what qualification to pursue next.

The progression model is currently best described as **skill-tree-like qualification discovery**, rather than a finalized class system. The player can see or discover opportunities and decide what they want to chase.

A meta control console may eventually let a player influence the kinds of situations they are seeking: a chaotic bloodbath, a prolonged invasion, a siege, a defensive action, or other scenario characteristics. This is an active design area, not yet a finalized rule.


raWWar intentionally allows players to choose useful work as gameplay.

### Mechanics

A player can qualify as a mechanic and repair damaged vehicles through physical interaction with tools and equipment.

### Research

A player can qualify for a research squad and operate laboratory equipment.

A conceptual interaction sequence might include handling a beaker, pouring it into another vessel, activating and lighting a burner, placing the vessel over heat, recording observations in a log, and continuing the experiment.

The intended feel is closer to physical task interaction than a conventional research menu.

### Construction

Buildings are constructed from extruded, preformed plates.

A player can transport plates, deliver materials, qualify as a construction squad, and physically follow construction directions.

The construction process itself is gameplay.

## 11. Air and space

The war is not restricted to ground combat.

Players can qualify to fly fighters, fly bombers, operate aircraft weapons, operate weapons consoles, operate tactical displays, serve at the helm, command ships, command fleets, or serve as admirals aboard command ships.

Space battles can involve massive fleets.

The same first-person principle applies: the player inhabits a station, cockpit, bridge, command center, or other physical role rather than switching to an abstract strategic interface.

## 12. The design consequence

The breadth of raWWar is intentional.

The question is not:

> “What class does the player pick?”

The deeper question is:

> “What do you want to do inside this war?”

The Experience should allow players to discover their own preferred form of participation.

## 13. Rules, procedures, and emergent military behavior

Soldiers obey the rules established by their commander and military organization.

Rules are therefore part of the commander's responsibility, not merely hard-coded assumptions. The organization can discover and formalize procedures in response to failures.

For example, if a squad is run over by a tank while attempting to cross a roadway, command may establish a minimum marching formation of two squads. One squad can provide a road guard while the other passes safely, with personnel posted to stop traffic before the formation crosses. The rule becomes part of how soldiers behave thereafter.

Marching groups can call cadences. Road guards, traffic control, formation requirements, staffing rules, and similar procedures should be capable of becoming observable behavior throughout the base.

This is a major part of the intended **alive** quality: the world should exhibit organization, memory, procedure, and adaptation rather than merely animating individual characters.

## 14. Intelligence, sabotage, and asymmetric participation

The war includes intelligence work and asymmetric roles.

Players can send spies and saboteurs, and can themselves become spies or saboteurs. These roles are intended to operate within the same qualification and organizational model as every other occupation.

The exact intelligence model, concealment rules, identification, counterintelligence, and consequences remain open.

## 15. Personal economy and exceptional equipment

The in-world arcade can contain games of chance using raWWar digital currency rather than real-world gambling value.

The currency can be used by soldiers to obtain personal equipment and customization beyond standard military issue, including armor, decals, vehicles, and potentially personally owned tanks.

A personally owned vehicle can be customized from a chassis and available parts, allowing the player to trade resources for performance, capability, and expression.

This is intended to create a personal layer inside the enormous military organization: the commander's requisition system sees force capacity, while the individual soldier also has personal goals, possessions, identity, and status.

The exact economy, balance, ownership rules, and gambling implementation remain to be designed.

## 16. The world should always be doing something

A central presentation goal is that raWWar should remain alive wherever it is observed.

The base should not become a static backdrop when the player looks away. Soldiers should be moving with purpose, aircraft and ground crews should execute procedures, scientists should conduct work, construction should progress, logistics should move materials, and organizations should respond to rules and events.

This is not a promise that every visible entity must always be simulated at maximum detail. It is a design goal for the Experience: **wherever the player observes, there should be evidence of a living system.**

## 17. Design discipline

The distinction must remain explicit:

- Canon — directly established by the creator.
- Candidate — a proposed interpretation requiring confirmation.
- Experiment — a gameplay or technical test intended to discover whether an idea works.

No implementation detail should silently become canon.


## 18. Campaign opening — lightning, palace, and the first lesson

The single-player campaign opens with the raWWar moniker.

After the moniker, a flash of lightning overilluminates the scene and establishes the player's first physical location: looking through a massive window from approximately eighty floors above a vast, active square.

Across the square stands an obscene Imperial palace. The square remains active despite rain and severe weather. The world outside the window is not a static background: while the player remains still and continues observing it, the exterior continues to exist and remain alive.

The opening deliberately teaches the player that observation matters.

The moment the player moves, attention returns to the interior. Loud science-fiction air-tight doors open, followed by the mechanical sound of two Imperial soldiers moving rapidly toward the player.

The soldiers reach the player and deliver the order: the Empress wants the Commander immediately in her office.

The player then begins to recognize that the location is the lobby of the faction headquarters. The major factions represent the balancing power opposing the Empress. Each major faction has an office or presence within the headquarters, accompanied by its own dogma, literature, recruiting materials, and other evidence of what that faction believes.

The opening is intentionally navigable rather than explanatory. The player can walk to one door, investigate another faction's presence and dogma, and explore the headquarters as a physical place.

But the game is also deliberately hard-learned.

When the Imperial soldiers order the Commander to proceed immediately, the player has approximately five seconds to comply. If the player does not proceed, both soldiers raise their weapons. They then provide a three-second countdown and execute the player.

There is enough warning to understand exactly what is happening, but not enough warning to make death feel like an arbitrary technical trap.

The intended sequence is:

1. Lightning.
2. Imperial palace.
3. Rain and living square.
4. The player observes the exterior.
5. Movement causes attention to return to the interior.
6. Air-tight doors open loudly.
7. Two Imperial soldiers approach at haste.
8. They order the Commander to report immediately to the Empress.
9. The player discovers the faction headquarters.
10. The player can investigate faction offices and their dogma.
11. Failure to obey the immediate order results in execution.

This opening establishes several truths without a tutorial:

- The world is physically present and continues to exist when observed.
- The player is a person with a position and obligations inside the world.
- Authority has consequences.
- Factions have distinct beliefs and recruiting identities.
- The player can explore, but exploration does not suspend danger.
- raWWar expects the player to learn through consequence rather than repeated safety rails.

## 19. Death is immediate loss

Death is not a temporary inconvenience.

In the single-player campaign, the player should not die. Death means the player has lost.

The player can lose through assassination, defeat by invading forces, destruction during an assault on an enemy position, stepping on a landmine, or other lethal failures.

The principle is intentionally severe:

> **Do not die.**

There should be no expectation that a death merely resets the player a few seconds backward and allows them to continue as though nothing happened.

Later campaign progression can introduce checkpoints so that a player does not necessarily have to replay the entire opening sequence after every failure. Checkpoints are a convenience for campaign progression, not a weakening of the underlying rule that death is immediate loss of the current attempt.

The opening assassination is therefore representative of the campaign's intended teaching style: the player receives enough information to recognize the danger, and then the world expects the player to act.

## 20. Major unanswered areas

The Bible is intentionally incomplete.

Areas still requiring elicitation include the setting and history of the war, factions, the premise of each universe, the player's initial relationship to a faction, death and replacement, individual soldier identity and persistence, injuries and recovery, recruitment, qualifications and training, command hierarchy, communications, logistics, economy, weapons and technology, research progression, base construction, vehicle ownership, aircraft and spacecraft rules, fleet organization, intelligence and fog of war, simulation scale, how much the world continues when nobody is watching, campaign victory conditions, persistent-universe victory or termination conditions, player-created organizations, moderation and governance, monetization, persistent-universe operating cost, cross-universe interaction, pocket dimensions, cooperative campaign structure, multiplayer rules, VR comfort, and how the Experience manifests in development environments.

This document should grow from conversation with the creator, not generic game-design assumptions.
