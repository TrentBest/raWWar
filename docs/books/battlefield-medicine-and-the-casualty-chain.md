# Battlefield Medicine and the Casualty Chain

![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg)

![SURPS Rulebook LOD 2](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg)

> How injured people move from the point of injury toward care, recovery, and a life beyond the event.

This raWWar-owned LOD 2 companion examines battlefield medicine as a distributed human system: recognition, communication, triage, collection, evacuation, treatment facilities, definitive care, rehabilitation, and long-term support. It is historical and game-design material, not clinical instruction. Real-world medical decisions and procedures belong to qualified professionals following current guidance.

## Related volumes

- [Medicine and Healing: History, Practice, and Play](medicine-and-healing.md)
- [Healing Across the Centuries](healing-across-the-centuries.md)
- [Bases and Encampments](bases-and-encampments.md)
- [The Art of Resupply](the-art-of-resupply.md)
- [Warfare and Organized Force](warfare-and-organized-force.md)

## 1. The casualty chain is a network

The phrase casualty chain suggests a sequence, but real systems branch, loop, pause, and fail. A person may receive immediate aid, move to a collection point, be reassessed, transfer between transport assets, enter a treatment facility, move to rehabilitation, or return to a community with continuing needs. Some stages may be absent; others repeat. The system must preserve identity, consent where possible, clinical handoff, and accountability across transitions.

## 2. Historical change without a progress myth

Compare ancient battlefield attendants and improvised evacuation with organized ambulance services, regimental medical structures, early-modern hospitals, nineteenth-century reforms, twentieth-century forward surgery and evacuation systems, and contemporary distributed trauma care. Institutions changed unevenly. Resources, transport, disease, doctrine, law, geography, and political commitment mattered. Avoid the simplistic claim that every later army cared better than every earlier one.

## 3. Recognition and reporting

A report should separate what a person directly observes from what they infer. Record source, timestamp, confidence, location estimate, patient identity confidence, and changes since the previous report. A sensor reading is not a diagnosis; an absent reading is not proof of absence. Conflicting reports should remain visible until reconciled.

## 4. Triage under scarcity

Triage is a context-dependent allocation process, not a moral ranking of human worth. Its criteria depend on the setting, available staff, transport, equipment, expected time to care, and governing protocol. A game should reveal the policy being simulated and allow players to understand why a decision changes when capacity changes. Never present an invented scoring rule as real clinical triage guidance.

## 5. Collection and evacuation

Collection points are interfaces between immediate surroundings and the wider care network. Their design must account for access, identification, shelter, communications, privacy, safe movement, staff capacity, and onward transport. Evacuation is not complete when a vehicle departs: the receiving destination must be known, capable, and ready to accept the patient.

## 6. Transport as a medical environment

Foot movement, litter teams, animal transport, wheeled ambulances, boats, aircraft, and futuristic protected carriers have different capacity, vibration, climate, communications, and access limits. A transfer can introduce delay, motion, exposure, separation from records, and handoff risk. The game model should represent these costs without turning the player into a real-world transport instructor.

## 7. Treatment facilities and role boundaries

Aid posts, field hospitals, ships' sickbays, mobile surgical facilities, civilian hospitals, and rehabilitation centers have distinct capabilities. A facility's capability depends on trained personnel, equipment, consumables, power, water, sterility systems, space, and referral routes. A label on a map does not make a facility operational. Role qualifications and supervision must be explicit in play.

## 8. Protected care and medical ethics

Medical personnel, patients, transports, and facilities may have special protections under applicable law. These protections do not erase difficult questions of identification, misuse, mixed use, security, or contested facts. The sourcebook should teach the distinction between a medical role and combat role, and should not reward attacks on protected care as ordinary tactical play. Legal details depend on applicable treaty, customary law, status, and facts; consult authoritative legal sources for real decisions.

## 9. The integrated medic HUD

The raWWar medic HUD is a decision-support interface with explicit layers:

- **Observed:** sensor values, visible signs represented in the fiction, direct reports, and their provenance.
- **Inferred:** tentative interpretations with confidence and the evidence supporting them.
- **Unknown:** missing, contradictory, stale, or unavailable information.
- **Care state:** recorded actions, responsible role, time, and reassessment status.
- **Movement state:** location, destination, transport assignment, estimated arrival range, and handoff acknowledgement.
- **System state:** battery reserve, sensor health, communications, equipment availability, and data integrity.

The interface never silently converts a sensor failure into a diagnosis or death declaration. It does not hide uncertainty behind a green icon. The medic remains responsible for interpreting the scenario within their character's qualification and the game's stated rules.

## 10. Ghost actions, Gestures, and haptic instruction

A ghost action is an instructional overlay: a translucent target pose, path, alignment, tool orientation, or sequence that shows the intended in-game action. The player performs the movement; tracking estimates what occurred; the system compares observed performance with the published rubric. This is a game mechanic and a training-design pattern, not a substitute for real surgical instruction.

For a fictional surgical scene, the HUD may display a ghost cut line and a tool-orientation envelope. The player activates the fictional scalpel, aligns it to the target, and maintains the requested movement while the simulation provides controlled haptic feedback. Gestures owns reusable embodied movement concepts; raWWar defines the task context, acceptable game-state transitions, and scoring rubric. The visual ghost, haptics, audio, and text must communicate the same instruction and provide accessible alternatives.

The scoring model should distinguish target alignment, path deviation, speed envelope, steadiness, tool activation state, sequence compliance, response to warning cues, and completion. It should show the player where the score came from. A single hidden score is not an adequate explanation. Tracking confidence must gate scoring: when the system cannot reliably observe the hand or tool, pause or mark the result unscored rather than penalize the player.

## 11. The fictional hypersonic scalpel

The hypersonic scalpel is explicitly **Fictional** equipment. Its ghost path is an interface instruction inside the game, not a claim that extreme sharpness makes surgery painless, safe, or simple. The fiction should still model calibration, energy, sterility, tool condition, operator qualification, patient state, safety interlocks, and failure modes. It is not a routine combat action or a magical substitute for diagnosis, anesthesia, surgical judgment, or recovery.

## 12. The exoskeleton cardiac-support system

In raWWar's setting, the soldier's exoskeleton/suit includes an integrated emergency cardiac-support assembly. The player's character may have pre-positioned adhesive or implanted-compatible leads routed to defined contact locations around the chest, with a dedicated controller and energy reserve. This is a **Fictional** integrated system, not a battery wired directly to a heart.

The game should distinguish monitoring, rhythm analysis, circulatory support, and a defibrillation function that only acts under its fictional device logic and stated eligibility conditions. A dramatic "heart starter" nickname can remain barracks humor, but the system must not teach that any stopped heart can be restarted by applying voltage. In real medicine, CPR and defibrillation are different interventions; defibrillation is not a treatment for every rhythm, and a flatline is not ordinarily corrected by a shock. Do not use real-world lead placement or electrical instructions as gameplay guidance.

## 13. State model for the simulation

Use an auditable state machine with states such as Reported, Located, Assessed, AwaitingCollection, InTransit, Received, UnderCare, Referred, Recovering, Rehabilitating, ReturnedToRole, and LongTermSupport. These are game-domain labels, not clinical categories. Transitions require evidence, an authorized actor, a timestamp, and a recorded handoff. Reassessment can return a case to an earlier routing state. A patient cannot disappear because a mission ended.

## 14. Failure, disagreement, and degraded operation

Scenarios should include dead batteries, occluded sensors, stale maps, broken radios, duplicate patient records, contradictory status reports, blocked routes, overloaded collection points, unavailable receiving facilities, and missing consumables. The system should degrade visibly and preserve uncertainty. Players can use alternate communication, request assistance, reroute, or record an unresolved state.

## 15. Logistics and medical readiness

Medical capacity is sustained by staffing, supplies, power, water, clean equipment, transport, maintenance, and receiving capacity. A fully stocked facility without qualified staff is not fully capable; a skilled medic without consumables may be constrained. Link to [The Art of Resupply](the-art-of-resupply.md) rather than duplicating the general logistics model.

## 16. Scenarios

- **Overloaded collection point:** several reports arrive at once; players must communicate uncertainty and coordinate capacity.
- **Handoff under interruption:** a transport arrives while the network is degraded and records disagree.
- **Ghost-line assessment:** a player performs a fictional, non-clinical tool-alignment exercise; scoring is gated by sensor confidence.
- **Battery reserve conflict:** the medic must preserve power for essential monitoring and communication in a fictional equipment system.
- **Civilian clinic:** military and civilian care needs compete for staff and supplies; lawful authority and humanitarian duties matter.
- **Evacuation route failure:** a planned destination becomes unavailable, forcing a recorded reroute and new handoff.

## 17. Evaluation and accessibility

Evaluate communication accuracy, provenance, uncertainty handling, safe role boundaries, handoff completeness, resource awareness, and response to system failure. For ghost actions, publish the rubric, allow calibration, offer visual/audio/text equivalents for haptics, permit intensity adjustment, and support seated or alternate-input play. Never conflate disability, device limitations, or tracking loss with poor performance.

## 18. Research and implementation boundary

Historical claims require source criticism and period-specific context. Clinical claims require current authoritative medical sources and expert review before any real-world instructional use. This book intentionally avoids procedural treatment instructions. Physics owns physical motion and energy simulation; Gestures owns reusable embodied movement; raWWar owns casualty-chain context, medic role design, and game evaluation; host systems own execution and persistence. SURPS remains opt-in.
