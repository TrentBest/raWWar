# raWWar — Two Campaigns, Diegetic Choice, and Embodied Procedure

**Status:** Creator-established design authority, with explicitly marked open implementation details.  
**Audience:** Game design, narrative, interaction, animation, simulation, and implementation.  
**Related:** [Game Design Bible](GAME_DESIGN_BIBLE.md), [Faction Design](FACTIONS.md), [Open Questions](OPEN_QUESTIONS.md).

## 1. One war, two campaigns

raWWar introduces two distinct single-player campaigns that inhabit the same underlying war:

- **Commander campaign — the RTS inside the FPS.** The player begins as the head of a faction and makes decisions from the top of its command structure. Strategic authority is experienced by a person physically present in the world, not by a detached cursor over a map.
- **Enlisted campaign — the FPS inside the RTS.** The player begins as the lowest enlisted grunt and advances through the chain of command until eventually becoming faction leader. The wider war continues around the player. Until the player reaches the relevant command levels, they generally go where they are sent rather than planning the war themselves.

The campaigns share the war's world and underlying systems, but they are not interchangeable presentations of the same role. Their starting positions confer different knowledge, authority, freedom, responsibilities, and exposure to consequences.

> **One war, two perspectives, different agency.** The Commander shapes the war from above; the recruit experiences its orders and consequences from below.

The creator's mathematical analogy is intentionally provocative: a common underlying model does not prove the two experiences are identical. If corresponding positions differ across an asserted equality, the correspondence and its meaning must be examined rather than waved away by calling the experiences “the same.” Preserve the tension between shared structure and different lived positions.

## 2. Enlisted campaign: fair, House choice, boot camp, and service

The enlisted campaign begins with the player as an unarmored citizen attending a fair. This opening gives the player time to observe the world and learn about the factions before committing.

An enlistment office presents a recruiting door for each faction/House. **Crossing the threshold into a House's door is the act of choosing that House.** It is the physical, diegetic equivalent of selecting a faction button in a conventional GUI. The door is not decoration around a separate menu; crossing the threshold is the selection interaction itself.

After choosing, the player appears in that faction's boot-camp barracks. On completing boot camp, the player appears at the faction's primary base. For this purpose, the primary base is the base where the faction leader is located. AI recruits may be assigned to other bases, including bases where the faction leader is absent.

This distinction helps control presentation cost without erasing the world: the design need not fully render every remote base merely because AI recruits exist there. Strategic simulation and appropriate levels of detail can still represent those bases. The world model must not confuse “not fully rendered” with “does not exist.”

## 3. Commander campaign: the storm, the square, and the House threshold

The Commander campaign opens during a rainy lightning storm. From a window, the player looks across a massive square toward the Imperial Palace. People moving far below appear like ants.

When the player turns away from the window, Imperial soldiers approach and inform them that the Empress requires an immediate audience in her office. The player then realizes they are in a lobby: doors that do not resemble elevator doors lead to faction offices.

The proposed lobby is three stories tall. Most offices are on the ground floor, with stairs leading to additional offices above and below. Its immense open volume is deliberate architecture, not wasted space. The lobby should make the player feel important. The Imperial Palace across the square should make the player feel insignificant. The contrast expresses two different kinds of power through space.

House selection is enacted by entering the chosen faction's office threshold. That threshold is a diegetic button, not a prompt to open a conventional faction-selection GUI. In the Commander opening, this choice sits under pressure: the Empress's summons is immediate, and the Imperial soldiers are not patient attendants.

The tension is intentional. Imperial soldiers regard the Commander's delay or disobedience as an opportunity to unleash their violence. If the player crosses into a faction office instead of complying with the summons, they would rather execute the player and report the disobedience than wait any longer. The exact selection timing, transition behavior, and fail-state presentation still need to be specified, but the threshold's meaning must not be replaced by a generic menu.

## 4. The Imperial soldiers

Imperial soldiers are the Empire's terrifying enforcement arm. Their loyalty is bred into them; their fierceness is renowned because they are raised as soldiers. They are meant to embody the nightmare every faction must face if it does not obey the Empress.

Their exoskeleton integration begins at five years old. As they grow, the armor must be surgically extracted and reinserted into a larger shell until growth stops. Installation is painful; removal is so much worse that the original installation becomes comparatively easy. Their lineage is shaped by thousands of years of intersecting bloodlines and selective breeding, more akin to breeding a guard dog for a specific purpose than ordinary military recruitment.

A nullification device numbs their pain and pleasure centers. They are consequently intended to be among the most bloodthirsty, brutal, and disciplined soldiers alive. Do not soften this into ordinary loyalty, generic elite troops, or merely intimidating uniforms: their bodies, upbringing, conditioning, discipline, and threat are all part of the Imperial system.

The exact biological, medical, ethical, and historical mechanisms remain to be detailed, but the established design purpose is clear: Imperial soldiers are not simply guards standing near a plot trigger. Their presence makes Imperial authority physically dangerous.

## 5. The soldier's exoskeleton is the player's interface to the world

The player is in a soldier's exoskeleton at all times. The player sees through its HUD. Vehicles and other equipment do not simply replace the player with an unrelated camera; the player operates physical controls and views the station's systems through the exoskeleton interface.

Every vehicle is intended to have physical actuators and control panels. Stations can differ according to the vehicle and task. The interaction should let the player *breathe what the soldier must do*: the player learns and performs procedures through embodied interaction, not merely through abstract button prompts.

For example, if a station requires both hands on a steering wheel, the player should see the ghost avatar's hands placed on the wheel. If a gear change is required, the ghost hand moves to the gear shift. The ghost demonstrates the required action, while the player attempts to reproduce it.

Piloting is likewise not just a camera feed. The soldier remains in their exoskeleton, and the station's own displays replace or occupy the relevant HUD presentation. A plain vehicle-camera view would be boring and would discard the physical act of operating the vehicle.

## 6. Training is a rotation through actual simulators

The training simulators are actual VR simulators for the recruit's intended duties, not abstract tutorial rooms that merely explain a control scheme. The player marches with their squad to the assigned training building. Inside, soldiers who staff the barracks help the player work out which VR module to use next. The training building and its staff are part of the world, not a replacement for it with a detached menu.

While the player is inside a VR training module, treat the player as **outside the passage of world time**. A qualification may represent a long period of instruction in the military world, but the player is not trapped in a simulator for that entire duration. Instead, training is divided into discrete hands-on sessions. The player can complete a session, return to ordinary gameplay with the qualifications already earned, and come back to training whenever they want.

If the player bails out of a VR module before completing its current session, that session's training is scrapped. No partial qualification credit is awarded for an abandoned session. This does not erase earlier completed sessions or qualifications. The player can qualify one session at a time and then go back into the living world, but **an incomplete qualification cannot be used** as if it were complete.

The simulation teaches the player how to operate and use the equipment they encounter. Training is not limited to a few tutorial weapons or one vehicle class; the intent is to teach the procedures relevant to the soldier's role and the world they will inhabit.

### 6.1 Squad-sized training and vehicle qualifications

A training barracks services **one squad at a time**. When the squad qualifies to operate a tank, its members train and qualify for each position in that tank as a squad. Qualification is therefore not merely a single “tank operator” unlock: the crew positions are distinct duties that each need to be learned and qualified for.

After boot camp, the player marches around the base with their squad and reports to a training barracks for the advanced-combat training assigned to that squad. This phase expands beyond the recruit's pistol to include weapons and equipment such as RPGs and rocket launchers. Squad-on-squad exercises—capture-the-flag-style training, for example—are part of this advanced combat phase.

Only after advanced combat has been passed does the jobs board become available to the player.

### 6.2 Qualification is not the same as experience

A qualification establishes that the soldier has been taught and passed the required training. It does not imply that the soldier has extensive experience performing every possible task in the field.

The ghost avatar can demonstrate the required actions during a real-world procedure, but its guidance is not a substitute for experience. When a soldier attempts an unfamiliar field repair, the availability and correctness of ghost guidance can depend on how often the soldier has performed that repair and on relevant nearby experience. A soldier who has never performed a repair—for example, using a plasma torch—may fail regardless of how closely they follow the ghost's displayed motions. They might instead cut unintended holes through the hull.

This is intentional: the ghost is a procedural aid, not a magic source of competence or a guarantee of success. Routine personal maintenance is not generally the soldier's responsibility; field emergencies can force a soldier to attempt repairs outside their normal duties.

The exact experience model, how nearby experience influences guidance, and how uncertainty is communicated remain to be designed. The established rule is that a perfect-looking ghost demonstration must not automatically make an inexperienced soldier competent.

## 7. The jobs board: diegetic career choice and interruptible training

After advanced combat, the player can inspect a physical jobs board and choose what work to pursue. Unlike a rigid modern military career path, the player may apply to another job **at any moment**. If the player is in the middle of a qualification, that training is paused at its current point rather than erased. If a job is listed and the player meets its prerequisites, they can apply; applications are typically accepted, but acceptance is not an unconditional guarantee.

The board does not present every role as equally available. A job that requires qualifications the player does not possess is visibly differentiated from an eligible selection. The player can still inspect that role and see the sequence of prerequisite qualifications required to reach it.

The selected job is marked by a green, rotating reticle. The selected destination and the prerequisite path are distinct visual states: the destination is the active choice, while the intervening qualifications are shown as the ordered route that must be completed first. The visual treatment must make it clear which job is selected, which options are eligible, and which steps remain.

This board is an in-world view of a qualification graph. It should not require the player to leave the world for a conventional out-of-character class-selection menu.

## 8. Single-player progression and multiplayer career freedom

The career system must account for a fundamental difference in how the player inhabits the world across modes.

### Single-player Enlisted campaign

The Enlisted campaign is a story-shaped rise from the lowest enlisted position toward faction leadership. The game has greater control over the sequence of opportunities and progression because it is guiding one player's lived story. The jobs board and qualification graph still expose prerequisites, but the campaign may direct the player through a narrower sequence than a multiplayer career would allow.

### Multiplayer

Every multiplayer player starts at the bottom as a soldier. The player chooses the class they want to train for. Training is undertaken in discrete VR sessions, with the player treated as outside the passage of world time while inside a module. The player can complete a session, return to gameplay with the qualifications currently earned, and come back to training whenever they want. If they bail out of an active module, that session's training is scrapped; earlier completed sessions and qualifications are not erased. An incomplete qualification cannot be used.

Once qualified in a class, the player may answer any job in that class. They may also answer jobs belonging to any other class for which they have qualified. Qualifications accumulate as real options, so the player can choose among jobs they are actually qualified to perform instead of being confined forever to one selected class. A job is an opportunity to answer, not a guarantee that the player will be assigned it; the existing job-acceptance and organizational constraints still apply.

This is not a contradiction in the world model. The single-player campaign deliberately constrains progression to carry its story; multiplayer offers wider career agency inside the same living military world. Shared simulation does not require identical player options.

## 9. Diegetic learning applies to every occupation

The ghost-avatar teaching model is not exclusive to infantry drills or vehicle operation. Research is also a job performed by a person in the world. Researchers undergo the training needed to qualify for their work; once qualified, they should not need to understand every other occupation, only what directly concerns their current responsibilities.

A player who chooses research encounters the experiments, equipment, procedures, and tasks of that research role. Ghost avatars can demonstrate what the researcher should do, just as they demonstrate a soldier's stance, a weapon procedure, or a vehicle control sequence. The player then performs the work.

The design principle is **local understanding, not omniscience**. Every occupation teaches the player how to do the work relevant to that occupation, through in-world activity. The player need not learn unrelated systems in advance merely to make progress in their chosen job.

## 10. General design principles

- **Diegetic choice:** Where practical, an important choice should be an action in the world. Crossing a threshold can be a clear selection event.
- **Embodied procedure:** The player learns what a soldier does by performing the soldier's actions through the exoskeleton interface.
- **Demonstration before abstraction:** Show the target behavior through the ghost avatar, then assess the player's attempt.
- **Instruction can respond to confusion:** Instructors and the guidance system can react to where the player actually is and what they have done, rather than assuming perfect compliance.
- **Training is interleaved:** A training day rotates through trainers and revisits topics over time rather than forcing one qualification to be completed in one sitting.
- **Squad qualifications are positional:** A squad can train together while qualifying for the distinct crew positions a vehicle requires.
- **Qualification does not equal mastery:** A credential, a demonstrated action, and accumulated experience are related but separate.
- **Career choice is an in-world action:** The jobs board exposes available work and its prerequisite path without leaving the fiction.
- **Career agency is mode-dependent:** Single-player may guide progression to carry its story; multiplayer starts everyone as a soldier and lets qualified players answer jobs in any class they have qualified for.
- **Learning is role-specific:** The player learns what their current occupation requires, not every job's procedures.
- **Agency follows role:** The Commander can plan; the recruit initially follows orders. Progression changes what the player is authorized and expected to decide.
- **Shared world, different experience:** Both campaigns can use the same underlying simulation without collapsing their differences in authority, perspective, or lived experience.
- **Presentation is not existence:** A remote base can remain part of the simulated world without always being fully rendered.
- **Hardware remains embodied:** Vehicles are operated through station-specific physical controls, actuators, panels, and exoskeleton HUD presentation.

## 11. Details still requiring design

The following are implementation or detailed-design questions, not permission to replace the established experience with a generic equivalent:

- Exact timing and failure-state handling when a Commander enters a faction office instead of obeying the summons.
- Whether and how the House threshold selection is presented in both campaigns when campaign setup or replay rules are considered.
- Ghost-avatar representation, tolerances, scoring curves, correction timing, accessibility, and how instructors decide the player is lost.
- Which dimensions are scored for each procedure; different tasks need not share one universal metric.
- The precise hardware and control mapping for each vehicle station.
- How training sessions are ordered within a qualification, how barracks staff guide the player to the next VR module, and what constitutes successful completion of a session.
- The complete qualification graph, including prerequisites, staffing demand, and the exact rules for job application acceptance.
- The exact differences between story-directed single-player progression and multiplayer class qualification, beyond the established rule that players may return to discrete training sessions when they choose.
- How accumulated experience and nearby expertise affect field-repair guidance, without allowing a ghost to guarantee success.
- How AI recruits, leader locations, remote-base simulation, and rendering fidelity are scheduled without implying that an unrendered base does not exist.

These details should be illuminated against the experience described above. They must not be filled with convenience-driven assumptions.
