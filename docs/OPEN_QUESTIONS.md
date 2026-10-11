# raWWar — Open Questions

Status: Living elicitation register; unresolved by design.
Owner: raWWar creator / game design.
Audience: Creator, design collaborators, narrative and systems specialists, and implementation agents.
Evidence rule: A question is not a decision. Do not fill a gap with generic assumptions or treat a proposed answer as canon without creator confirmation.

This is deliberately a living interrogation list.

The purpose is to expose what the creator knows but has not yet articulated, not to fill the gaps with generic game design.

## Highest-priority questions

### 1. What is the war?

What conflict creates the world?

Who is fighting?

Why?

What does each side believe it is protecting or achieving?

### 2. What is the player?

The campaign opening establishes the player as a **Commander** already embedded in a faction headquarters, but the player's origin, prior history, faction identity, and path into command remain to be defined.

How did the player become Commander?

What faction do they represent?

What relationship do they have with the Empress?

What was happening immediately before the opening scene?

### 3. What makes a soldier matter?

What persists about an individual?

What can make one soldier memorable?

### 4. What does death mean?

Campaign death is an immediate loss of the current attempt. The player is not expected to survive every encounter, but the design expectation is that they **should not die**.

What persists after a campaign loss?

What does a later checkpoint preserve?

How do death and replacement differ between campaign, cooperative, and persistent modes?

### 5. What makes a universe end?

Can a faction actually win?

Can a universe reach peace?

Can it collapse?

Can it be restarted?

### 6. How autonomous is the war?

What happens when no player is looking?

Do armies move?

Do researchers continue?

Do factories produce?

Do enemies adapt?

### 7. What is the technology ladder?

What can be researched?

How does a new technology move from research to demonstration to qualification to deployment?

### 8. What is command authority?

How does a commander's decision propagate through officers, squads, and individual soldiers?

How much can subordinates reinterpret orders?

### 9. What is the relationship between skill and qualification?

Is qualification a certification, a progression path, a permission system, or all three?

### 10. How large is the living simulation?

What population and simulation levels are authoritative? How are hundreds of thousands of visible soldiers represented while preserving individual identity and meaningful behavior?

### 11. What is the qualification graph?

Which qualifications exist? Which require other qualifications, rank, officer status, training facilities, or organizational need?

### 12. How do rules evolve?

Who can establish, modify, revoke, and enforce military procedures? Can organizations learn from failures and formalize new rules?

### 13. What does readiness mean?

How do staffing, training, fatigue, maintenance, supplies, and qualifications combine into operational readiness?

### 14. What is the personal economy?

What can soldiers own? What can they buy with raWWar digital currency? How are customized vehicles balanced against military equipment?

### 15. How alive is the world when unobserved?

Which systems continue continuously, which are simulated at lower fidelity, and which are abstracted until observation makes detail necessary?

### 16. What is the first ten minutes?

The opening campaign sequence is now defined at a high level: moniker, lightning, Imperial palace and living square, observation-driven exterior persistence, transition into the faction headquarters, Imperial soldiers ordering the Commander to report to the Empress, faction offices and dogma available for exploration, and an early assassination lesson for failure to obey.

The opening should demonstrate the philosophy of raWWar before explaining it.

What happens immediately after the player obeys and goes to the Empress?

What is the first actual mission?

What does the player know about the factions and the war at that point?


### 17. What is the player equipment progression?

What is the canonical entry-level exoskeleton?

Which additions are military issue, earned, purchased, discovered, or persistent-universe exclusive?

How much customization is available in campaign, cooperative campaign, and persistent play?

How does equipment affect qualification, readiness, maintenance, power, and identity?

### 18. How large is cooperative campaign?

What scenarios support two players? What changes at 8, 32, 64, or approximately 128 players?

Can cooperating players belong to different factions?

How are objectives, information, command authority, and conflicting interests handled?

### 19. What is the raWWar visual language?

What makes a raWWar soldier, vehicle, facility, weapon, and faction recognizable as belonging to this universe?

Which elements are modular, procedural, hand-authored, or generated?

### 20. What is the content production pipeline?

How are original soldiers, vehicles, environments, animation, audio, and effects authored, validated, versioned, and delivered to the Experience and Workshop Renderer?

### 21. How is the Gesture system authored and integrated?

How is Gesture authored, represented, evaluated, and individualized? How is it integrated with the Renderer and MicroBundle Gesture Providers?

### 22. How do the two campaigns enact House choice and authority?

The creator has established two distinct single-player campaigns:

- **Commander campaign:** The player starts as faction leader. The opening is a rainy lightning storm, with the player looking across a massive square toward the Imperial Palace. Imperial soldiers demand an immediate audience with the Empress. The player then discovers that the monumental, three-story lobby contains faction offices.
- **Enlisted campaign:** The player starts as an unarmored citizen at a fair, has time to learn about the Houses, chooses a faction by crossing the threshold of that House's recruiting door, enters its boot-camp barracks, and appears at the faction leader's primary base after completing boot camp. The player rises from the lowest enlisted rank toward faction leadership while the wider war continues.

Crossing a House door's threshold is the faction-selection interaction itself—the diegetic equivalent of clicking a faction button. In the Commander opening, the choice is under immediate pressure: Imperial soldiers regard entering a faction office instead of obeying the Empress's summons as disobedience and would rather execute the Commander than wait.

The lobby and palace deliberately contrast their scale and emotional effect: the vast lobby should make the player feel important; the Imperial Palace should make a person feel insignificant.

See [Campaign Modes and Diegetic Training](CAMPAIGN_MODES_AND_DIEGETIC_TRAINING.md).

**Remaining design questions:**
- How exactly is the threshold event, House selection, and the Empress's summons resolved in the Commander campaign, including the order of events and any resulting fail state?
- How are campaign selection, replay, and return-to-campaign handled without weakening the physical meaning of the threshold?
- What happens immediately after the Commander obeys the summons? What is the first actual mission?
- What does the player know about the factions and the war at the start of each campaign?

### 23. How does embodied station control work across vehicles and procedures?

The player is always operating through a soldier's exoskeleton and its HUD. Vehicles have physical actuators and control panels, and stations have task-appropriate controls. The player's view should not collapse into a generic vehicle camera; the player sees and operates the station through the soldier's embodied interface.

A ghost avatar demonstrates intended actions. During boot camp, for example, it shows the recruit where to stand, how to handle and disassemble a weapon, and the required positioning, timing, and rotation. The player attempts to follow the demonstration; closer performance earns a better action score. The guidance can react to confusion: an instructor may call out a lost recruit, after which the ghost updates to show the correct target.

This establishes the teaching and interaction model, but not every low-level station contract.

- Which movement, pose, timing, rotation, contact, and sequence dimensions matter for each procedure?
- How are scoring tolerances and performance curves chosen per task?
- How does the system infer that the player is lost, and when should an instructor or ghost update the instruction?
- What are the exact actuator/control mappings, constraints, and feedback mechanisms for each vehicle station?
- How do accessibility and alternate input devices preserve the intended embodied procedure?

Do not generalize one station's control scheme to every vehicle, and do not treat a ghost demonstration as the player's completed action. The ghost shows the target; the player must perform the procedure.

### 25. How does career progression differ between single-player and multiplayer?

The creator has established a deliberate mode distinction. The single-player Enlisted campaign guides one player's progression more tightly to carry the story of rising from the bottom of the military hierarchy. Multiplayer begins every player at the bottom as a soldier. A multiplayer player chooses a class to train for through discrete VR sessions; the player is outside the passage of world time while training, can return to gameplay between sessions, and can resume training whenever they choose. Abandoning an active module scraps that session's training, while earlier completed sessions and qualifications remain intact. Incomplete qualifications cannot be used. Once qualified, the player may answer any job in that class or any other class for which they have qualified.

The same underlying world and qualification system can support both modes without granting identical career freedom.

**Remaining design questions:**
- What counts as successfully completing each VR training session, and how does the barracks staff direct a recruit to the next module?
- Does multiplayer class qualification establish eligibility for every job in that class, or do some jobs require additional position-specific qualifications, experience, rank, or staffing conditions?
- How are multiple qualified classes displayed on the diegetic jobs board?
- How should the world present a job opportunity when the player is qualified but another applicant or organizational need may determine assignment?
- How much of the single-player Enlisted progression is fixed for narrative pacing, and where may the player choose alternate branches without breaking the intended story?

### 24. How do interleaved training, qualifications, experience, and career changes work?

The recruit marches with their squad to the assigned training building, where the soldiers staffing the barracks help them choose the appropriate VR module. The player is outside the passage of world time while inside a module. Training is divided into discrete, ordered hands-on sessions. Successfully completed sessions accumulate toward the unfinished qualification and remain recorded across gameplay breaks and job/class changes. If the player bails out of an active module, the current session's training is scrapped; prior completed sessions and qualifications remain intact. Incomplete qualifications cannot be used. Long qualifications can therefore be completed across multiple visits without trapping the player in VR for the full in-world duration.

Qualification progress is explicit and sequential, not a binary flag or a pool of interchangeable lessons. If fighter-pilot qualification requires ten sessions, progress is tracked as 1/10 through 10/10; every soldier at 3/10 has completed the same first three prescribed sessions in the same order. Changing jobs or classes does not erase successfully completed sessions toward an unfinished qualification. Only abandoning the active module scraps that session's work.

The soldier's current role determines which role-specific insignia and rating badge they display, and the qualification level reached is reflected in the appropriate markings. The exact visual designs and conventions remain to be specified.

A training barracks serves one squad at a time. A squad qualifying to operate a tank must qualify for each crew position, not merely acquire a single generic tank qualification. After boot camp, the squad marches around its base, reports to its assigned training barracks, completes advanced combat training with a wider set of weapons and squad-on-squad exercises, and then gains access to the jobs board.

The jobs board is diegetic. The player can apply to another listed job at any time; current training pauses rather than being discarded. Applications are typically accepted, but not guaranteed. Roles whose prerequisites are missing are visibly distinguished, and selecting one shows the ordered qualification path needed to reach it. The selected destination uses a green rotating reticle, distinct from the prerequisite path.

The same ghost-avatar model teaches all occupations, including research. A qualified researcher needs to learn the procedures relevant to their own work, not every other occupation.

Qualification and experience are separate. A ghost can demonstrate a field-repair procedure, but its guidance may depend on the soldier's own repetitions and relevant nearby experience. Following a ghost does not guarantee success at an unfamiliar repair; a soldier who has never used a plasma torch might cut unintended holes in a hull instead. Soldiers do not normally perform their own maintenance, but field emergencies may force them to try.

**Remaining design questions:**
- What are the complete qualification families and prerequisite edges, and which roles have additional rank, staffing, aptitude, or organizational requirements?
- What counts as successful completion of a VR training session, and how do barracks staff help the player choose the next module?
- What determines whether a job application is accepted when the player meets its visible prerequisites?
- What information is shown for locked or unqualified jobs without turning the board into an out-of-world menu?
- How are squad members distributed across each vehicle's required crew positions, and how is qualification recorded for each position?
- How does prior personal experience and nearby expertise affect the ghost's repair guidance? What is shown when guidance is uncertain or unavailable?
- How are simulator performance, qualification status, field experience, and actual task outcome represented separately?
- What are the exact insignia and rating-badge designs, placement rules, and visual conventions for each role and qualification level?
- What service medals can soldiers earn, what deeds or service qualify for each award, and how do medal designs and chest-wearing conventions differ by faction?
- Which training procedures are shared across factions, and which are faction-specific?



### 26. How are squad standards, totems, and rank recognition expressed visually?

A candidate design places a tall physical squad standard or totem on the back of the lowest-ranking soldier—the fourth member in the four-soldier squad concept. It should remain visible as a real object moving through dust, smoke, and battlefield motion, not as a floating UI label.

Questions to resolve:
- Is the squad always four soldiers, and is the standard-bearer always the lowest-ranking member?
- Is the standard a flag, a rigid totem, or a configurable combination?
- How do faction identity, squad identity, command/rank markings, role insignia, qualification badges, and service medals remain visually distinct?
- Can standards be lost, captured, damaged, replaced, or used tactically?
- What are the visibility and silhouette requirements at near, middle, and far Event Horizons?
- Which visual cues are universal and which are faction-specific?

The visual concept may draw broad inspiration from historical military standards and science-fiction unit silhouettes, but final designs must be original.
