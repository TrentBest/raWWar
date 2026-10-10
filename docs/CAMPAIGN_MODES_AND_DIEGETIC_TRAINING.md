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

The tension is intentional. Imperial soldiers regard the Commander’s delay or disobedience as an opportunity to unleash their violence. If the player crosses into a faction office instead of complying with the summons, they would rather execute the player and report the disobedience than wait any longer. The exact selection timing, transition behavior, and fail-state presentation still need to be specified, but the threshold's meaning must not be replaced by a generic menu.

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

## 6. Boot camp and the ghost-avatar teaching system

Boot camp teaches procedures by showing the player what the soldier should do and then letting the player perform it.

A ghost avatar demonstrates a target position, pose, movement, timing, rotation, or manipulation. The player tries to follow it. The closer the player's action is to the ideal demonstration, the higher the action's score and resulting performance.

Example: the drill sergeant commands, “Fall in.” The system highlights the expected location. That first indication does not have to be correct. If the player is visibly lost, the drill sergeant may react—“What is your problem, recruit?”—and the system then provides an updated ghost position, this time showing the correct place. The instruction, the player's attempt, and the instructor's response all belong to the world.

The same principle applies to weapon disassembly, vehicle operation, and other physical procedures. The ghost is a demonstration of intended behavior, not a replacement for the player's action. Position, timing, orientation, sequencing, and interaction quality can all contribute to evaluation where relevant.

This system should teach through doing. A tutorial overlay may explain a rule when needed, but it must not replace the central embodied loop: observe the ghost, attempt the procedure, receive a result, and improve.

## 7. General design principles

- **Diegetic choice:** Where practical, an important choice should be an action in the world. Crossing a threshold can be a clear selection event.
- **Embodied procedure:** The player learns what a soldier does by performing the soldier's actions through the exoskeleton interface.
- **Demonstration before abstraction:** Show the target behavior through the ghost avatar, then assess the player's attempt.
- **Instruction can respond to confusion:** Instructors and the guidance system can react to where the player actually is and what they have done, rather than assuming perfect compliance.
- **Agency follows role:** The Commander can plan; the recruit initially follows orders. Progression changes what the player is authorized and expected to decide.
- **Shared world, different experience:** Both campaigns can use the same underlying simulation without collapsing their differences in authority, perspective, or lived experience.
- **Presentation is not existence:** A remote base can remain part of the simulated world without always being fully rendered.
- **Hardware remains embodied:** Vehicles are operated through station-specific physical controls, actuators, panels, and exoskeleton HUD presentation.

## 8. Details still requiring design

The following are implementation or detailed-design questions, not permission to replace the established experience with a generic equivalent:

- Exact timing and failure-state handling when a Commander enters a faction office instead of obeying the summons.
- Whether and how the House threshold selection is presented in both campaigns when campaign setup or replay rules are considered.
- Ghost-avatar representation, tolerances, scoring curves, correction timing, accessibility, and how instructors decide the player is lost.
- Which dimensions are scored for each procedure; different tasks need not share one universal metric.
- The precise hardware and control mapping for each vehicle station.
- How AI recruits, leader locations, remote-base simulation, and rendering fidelity are scheduled without implying that an unrendered base does not exist.

These details should be illuminated against the experience described above. They must not be filled with convenience-driven assumptions.
