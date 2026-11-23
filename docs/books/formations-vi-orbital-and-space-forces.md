# Formations VI: Orbital and Space Forces

![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg)

![SURPS Rulebook LOD 2](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg)

> A space formation is a time-dependent relationship among trajectories, vehicles, people, infrastructure, information, and authority.

This LOD 2 sourcebook treats orbital and space formations as a distinct domain. It focuses on the organization and coordination of spacecraft, crews, autonomous systems, stations, service vehicles, habitats, and support networks. It complements [Orbital Warfare and Space Infrastructure](orbital-warfare-and-space-infrastructure.md), which addresses the wider security, governance, and infrastructure domain. This book is an educational and simulation-design framework, not a guide to real-world spacecraft operations or military employment.

## Related sourcebooks

- [Formations I: Land Forces](formations-i-land-forces.md)
- [Formations II: Naval Forces](formations-ii-naval-forces.md)
- [Formations III: Air Forces](formations-iii-air-forces.md)
- [Formations IV: Mounted and Cavalry Forces](formations-iv-mounted-and-cavalry.md)
- [Formations V: Mechanized and Armored Forces](formations-v-mechanized-and-armored-forces.md)
- [Orbital Warfare and Space Infrastructure](orbital-warfare-and-space-infrastructure.md)
- [Interstellar Logistics and Strategy](interstellar-logistics-and-strategy.md)
- [Planetary Defense and Habitat Conflict](planetary-defense-and-habitat-conflict.md)
- [Autonomous Systems and Machine Agency](autonomous-systems-and-machine-agency.md)
- [The Art of Resupply](the-art-of-resupply.md)
- [Bases and Encampments](bases-and-encampments.md)

## 1. Scope and conceptual boundary

Space formations are organized relationships among assets and people operating in an environment where motion is governed by orbital mechanics and where energy, time, information, and access are tightly coupled. The formation may be temporary, such as a coordinated observation campaign, or persistent, such as a station's resident and visiting service network.

A formation is not necessarily a line, wedge, column, or visual cluster. Its members may occupy different orbits, operate at different altitudes, share a relay network, service one another, or coordinate through delayed ground control. The defining feature is a mission-bounded relationship with explicit authority, dependencies, timing, and constraints.

This volume does not assume that spacecraft are naval vessels, that orbital paths are roads, or that spacecraft can stop and turn like aircraft. Naval, air, and land formation concepts may provide useful organizational analogies, but their physical assumptions must not be imported uncritically.

## 2. Evidence labels: established, extrapolated, speculative, fictional

Every future-facing treatment should label its claims:
- **Established:** supported by known physics, documented engineering, or currently demonstrated and deployed capabilities.
- **Extrapolated:** a plausible extension of existing principles or systems whose operational details remain uncertain.
- **Speculative:** dependent on unresolved engineering, economics, governance, or science.
- **Fictional:** an explicit setting rule invented for a particular experience.

The labels apply to individual claims, not entire books or civilizations. A scenario can combine established orbital dynamics with speculative propulsion and fictional political institutions, provided the boundaries are visible. Never allow a fictional drive system to silently rewrite the known behavior of every other object.

## 3. The formation as a multi-layer system

A useful formation model separates:
1. **Physical assets:** spacecraft, stations, habitats, depots, relays, payloads, and service platforms.
2. **People:** crews, controllers, maintainers, passengers, researchers, rescue personnel, and civilians.
3. **Trajectories and states:** orbit, attitude, relative motion, configuration, propellant or energy, thermal condition, and uncertainty.
4. **Organization:** mission roles, command authority, operator responsibility, task assignments, and coalition relationships.
5. **Support:** communications, navigation, tracking, ground stations, launch access, repair, logistics, life support, and emergency services.
6. **Governance:** ownership, registration, jurisdiction, access rights, safety coordination, liability, and conflict-management arrangements.

No one layer substitutes for another. A spacecraft can be physically healthy but unable to perform its mission because its crew, data link, authorization, or support network is unavailable.

## 4. Orbital mechanics is not formation decoration

Orbital motion is determined by gravity and velocity. Relative position and velocity evolve over time; changing an orbit requires a change in energy and momentum. Altitude alone does not determine whether two spacecraft are close, converging, diverging, or able to rendezvous.

Formation descriptions should therefore be time-stamped and tied to a declared reference frame and model. Record what is observed, what is predicted, the prediction epoch, uncertainty, and the source of the estimate. A static diagram is a snapshot, not a complete account of the formation.

Physics owns authoritative dynamics, coordinate transforms, propagation, collision modeling, and environmental effects. raWWar owns the mission meaning, organizational relationships, and scenario constraints. This book intentionally avoids real-world maneuver recipes, intercept calculations, or operational orbital parameters.

## 5. Relative motion, rendezvous, and proximity

Rendezvous and proximity operations are not equivalent to driving two vehicles toward one another. Relative motion depends on orbital state, timing, uncertainty, navigation quality, vehicle capability, and agreed safety procedures. A small-looking separation on a diagram does not establish safe approach or a stable relationship.

For simulation, represent proximity as a state with uncertainty and explicit authorization, not as a simple distance threshold. Distinguish planned rendezvous, cooperative approach, station-keeping relationship, unplanned conjunction, emergency separation, and loss of relative-state knowledge. The detailed physical controller belongs in the appropriate engineering and physics systems, not in this domain manual.

## 6. Formation families and mission roles

Possible space-formation roles include:
- crewed transport and transfer vehicles;
- uncrewed cargo and logistics craft;
- scientific and observation platforms;
- communications and navigation relays;
- servicing, inspection, and repair vehicles;
- stations, habitats, and orbital depots;
- rescue and emergency-support assets;
- launch, tracking, and ground-support organizations;
- autonomous inspection and maintenance systems;
- planetary or lunar surface-to-orbit support networks.

These categories describe possible roles, not a universal table of organization. A mission may be civilian, commercial, scientific, diplomatic, emergency, military, or mixed. Dual-use status should be documented rather than assumed from a vehicle's appearance.

## 7. Station keeping and persistent relationships

A persistent formation may be organized around a station, habitat, depot, relay network, or shared mission. Its members need not remain in one geometric cluster. Membership may instead depend on service commitments, scheduled access, data exchange, rescue obligations, or shared governance.

Record the relationship's scope, start and end conditions, responsible operators, permitted tasks, and emergency exceptions. A visiting vehicle is not automatically under the station operator's command. Docking, berthing, servicing, and access are distinct states with separate authorization and safety dependencies.

## 8. Three-dimensional geometry and time windows

Space formations operate in three dimensions and over time. Their geometry may involve orbital planes, phase relationships, relative trajectories, line-of-sight, communication windows, lighting, thermal constraints, and access to a target or service location.

A visualization should not suggest that a two-dimensional projection fully captures the state. Show reference frame, epoch, scale, uncertainty, and whether a line represents a physical trajectory, communication link, organizational relationship, or merely a conceptual connection. Keep those link types visually distinct.

## 9. Command, control, and authority

Space operations may involve vehicle commanders, mission control, commercial operators, national authorities, station operators, payload owners, autonomous systems, and partner organizations. Authority is distributed and context-dependent.

Model the difference between:
- legal authority;
- contractual responsibility;
- mission command;
- vehicle safety authority;
- technical control;
- recommendation from an automated system;
- emergency decision rights.

Orders and requests should preserve issuer, recipient, scope, time, expiry, acknowledgement, and provenance. A communications link does not itself grant authority. A ground controller's ability to transmit a command does not mean the command is lawful, safe, or accepted by the receiving crew.

## 10. Communications delay, interruption, and autonomy

Communication latency and outages shape what can be coordinated centrally. Delay depends on distance and network architecture; signal availability also depends on geometry, relays, ground infrastructure, interference, and equipment health.

A formation model should represent messages as events with send, receive, acknowledgement, and expiry times. Do not treat every participant as sharing one instantaneous world state. When communications are interrupted, local agents may continue only within previously authorized boundaries and available information.

Autonomy must be explicit about its delegated scope, safety envelope, uncertainty, override conditions, and accountability. An autonomous asset is not an omniscient participant. See [Autonomous Systems and Machine Agency](autonomous-systems-and-machine-agency.md).

## 11. Crew, passenger, and civilian identity

People remain distinct from the spacecraft that transports or houses them. Track crew assignment, duty status, qualifications, fatigue, health, consent, evacuation options, and handoff responsibilities. A vehicle's destruction or loss of contact must not silently decide the fate of every person associated with it.

Habitats and stations are homes, workplaces, laboratories, and public infrastructure. Residents are not merely mission payloads. Their rights, agency, access to information, and ability to refuse dangerous work must be visible in the setting and simulation.

## 12. Propellant, energy, and consumables

Spacecraft endurance depends on finite resources and support. Depending on vehicle and mission, relevant resources include propellant, electrical energy, thermal-control capacity, life-support consumables, food, water, breathing atmosphere, spare parts, data storage, and crew time.

Do not collapse these into one generic fuel bar. Resource compatibility, storage, transfer capability, contamination, degradation, delivery windows, and reserve policy matter. A vehicle may have electrical power but lack propellant; a habitat may have stored supplies but insufficient recycling or repair capacity.

The Art of Resupply owns the wider logistics concepts. This book defines the space-domain requirements and consequences without inventing a universal logistics engine.

## 13. Thermal, radiation, and environmental constraints

Spacecraft and habitats face thermal management requirements, radiation exposure, micrometeoroid and orbital-debris hazards, vacuum, lighting cycles, and limited opportunities for inspection or repair. These conditions vary with orbit, shielding, vehicle design, activity, and time.

Represent environmental exposure as a modeled condition with uncertainty and provenance, not as arbitrary damage applied for dramatic effect. A habitat's environmental-control systems are life-critical infrastructure. Their failure affects residents, visitors, responders, and the wider support network.

Physics owns environmental simulation. Medical consequences and care systems belong with the medicine sourcebook family. Do not provide real-world radiation dose limits, life-support repair instructions, or spacecraft safety procedures as gameplay directions.

## 14. Docking, berthing, servicing, and shared infrastructure

Docking or berthing changes the relationship between assets: mechanical interfaces, electrical power, data, atmosphere, thermal systems, and responsibility may become shared or coupled. The exact capabilities depend on compatible designs and explicit system interfaces.

Represent connection states carefully: scheduled, authorized, approach in progress, connected, services enabled, isolated, disconnected, and emergency separation. Each transition should have prerequisites, responsible parties, timestamps, and failure outcomes. A visual contact between two models is not proof of a functioning or safe connection.

Servicing and repair may require tools, spares, trained personnel, robotic systems, work areas, and return-to-service verification. Preserve component identity and maintenance history.

## 15. Logistics, transport, and industrial support

Space logistics begin on the ground and continue through launch, transfer, storage, maintenance, and disposal. Launch facilities, manufacturing, supply chains, tracking networks, ground stations, trained labor, and regulatory approvals are part of the formation's real support footprint.

A depot is not an infinite inventory. A cargo vehicle is not a magic refill interaction. A delayed launch can affect a station's maintenance plan, a rescue reserve, a scientific schedule, and civilian supply. Model lead time, capacity, compatibility, risk, ownership, and competing demand.

Interplanetary and interstellar travel add longer distances, longer communication delays, greater autonomy requirements, and potentially very different propulsion assumptions. See [Interstellar Logistics and Strategy](interstellar-logistics-and-strategy.md); do not conflate its speculative transport premises with established orbital operations.

## 16. Rescue, evacuation, and emergency response

Rescue is a coordinated service with its own authority, resources, access constraints, and risks. A rescue craft may need to coordinate with a station, ground network, medical team, and multiple operators. A rescue request does not guarantee immediate access or successful recovery.

Scenarios should preserve uncertainty: the location or condition of a crew may be unknown; communications may be intermittent; environmental conditions may change; rescue capacity may be committed elsewhere. Medical assessment, treatment, and long-term recovery belong to the medicine domain. Rescue and salvage are not interchangeable.

## 17. Debris, contamination, and externalities

Orbital debris and accidental damage can affect operators who were not parties to the original event. A formation's decisions may create long-lived hazards, interfere with shared services, or impose costs on future missions and generations.

Track object provenance, confidence, conjunction uncertainty, responsibility disputes, mitigation status, and the consequences of fragmentation or uncontrolled disposal. Do not portray debris as a convenient visual obstacle field with no ownership or political context. Avoid procedural descriptions of creating debris or disabling real spacecraft.

## 18. Civilian, commercial, and scientific participation

Space is not exclusively a military theater. Commercial operators, scientific institutions, civil agencies, international partnerships, passengers, and future settlement communities may share infrastructure and operating environments.

Distinguish the operator, owner, customer, beneficiary, regulator, and affected public. Their interests may diverge. A service disruption can affect navigation, communications, weather observation, emergency response, financial transactions, environmental monitoring, and scientific continuity. Civilian impact should be traced through actual dependencies rather than treated as a generic reputation penalty.

## 19. Security, law, and escalation

Space formations exist within legal and political frameworks. Registration, jurisdiction, state responsibility, liability, rescue obligations, spectrum coordination, access, and the law applicable during armed conflict require careful treatment. This is educational material, not legal advice.

Ambiguous intent, dual-use systems, uncertain attribution, close approaches, interference, and rescue disputes can escalate. Model verification, incident communication, third-party mediation, transparent logs, and deconfliction arrangements as meaningful capabilities. Do not make conflict inevitable or assume that every state, company, crew, or habitat acts as a single unified actor.

For the broader conflict and infrastructure analysis, use [Orbital Warfare and Space Infrastructure](orbital-warfare-and-space-infrastructure.md).

## 20. Training, visualization, and ghost formations

A training view may show predicted trajectories, assigned roles, communication relationships, service dependencies, access windows, and uncertainty envelopes as ghost objects or overlays. The visualization must distinguish observed state from predicted state and conceptual organization from physical geometry.

A player or trainee should be able to inspect why a relationship is valid, uncertain, expired, or unauthorized. Scoring can evaluate mission-role assignment, communication acknowledgement, resource awareness, safe-state recognition, handoff quality, and response to changed information. Do not score a participant against tracking data whose confidence is inadequate.

This is a visualization and scenario-design pattern, not operational flight training. Physics owns motion and environmental truth; Gestures owns reusable embodied interaction; raWWar owns domain context and assessment.

## 21. Simulation states and event history

Represent each asset with a stable identity and explicit state: mission role, operator, crew, orbit reference, epoch, configuration, health, resources, communication state, legal status, and confidence. Keep mission membership separate from physical proximity.

Events should include assignment, release, task request, authorization, message transmission, acknowledgement, state observation, predicted change, resource transfer, service connection, maintenance, emergency declaration, rescue, and mission completion. Preserve who asserted each fact and when. Corrections should append provenance rather than silently overwrite history.

A formation can reorganize, split, merge, go silent, lose support, or dissolve. These are organizational transitions; they do not require every asset to be destroyed or every relationship to persist.

## 22. Failure modes and graceful degradation

Possible failures include loss of communications, stale navigation data, power shortage, thermal-control degradation, propulsion faults, attitude-control limitations, sensor disagreement, life-support emergencies, debris events, delayed cargo, unavailable ground stations, and contested authority.

Model local failure and cascading dependencies separately. A failed relay may affect several users without physically damaging them. A habitat's loss of one service may trigger conservation and evacuation decisions. A spacecraft with uncertain state should not be declared destroyed solely because telemetry stopped.

A robust scenario makes recovery, redundancy, manual review, safe modes, mutual aid, and transparent uncertainty meaningful. Failure should create consequences, not arbitrary total-system resets.

## 23. Scenario frames

- **Delayed rendezvous window:** an unexpected service delay changes the timing and availability of a cooperative mission; participants must update shared assumptions rather than assume the original schedule still holds.
- **Relay outage:** multiple operators lose access to a shared communications service; civilian, scientific, and mission impacts diverge.
- **Rescue priority dispute:** a limited rescue capability is requested by two parties with different evidence and urgency claims.
- **Station maintenance backlog:** a habitat remains occupied while critical service work competes with cargo, crew time, and safety reserves.
- **Uncertain conjunction:** tracking sources disagree; the formation must preserve uncertainty and use established coordination channels rather than treating one prediction as fact.
- **Autonomy boundary:** an autonomous servicing asset receives conflicting or stale instructions; the scenario tests delegated authority and auditability.
- **Shared depot allocation:** commercial, scientific, and emergency users dispute a scarce resource, requiring transparent custody and governance.
- **Debris responsibility:** an event creates risk for uninvolved operators; attribution is uncertain and the political response may be more consequential than the physical event.

## 24. Research method and implementation boundaries

Research should combine orbital mechanics, astrodynamics, spacecraft systems engineering, human factors, space traffic coordination, resilience engineering, international law, and space policy. Record the date and scope of every contemporary capability claim. Separate demonstrations, prototypes, operational services, proposals, and fictional systems.

Ownership:
- **raWWar:** space-formation roles, organizational meaning, historical and political context, scenario design, and evaluation.
- **Physics:** authoritative physical dynamics, reference frames, motion, collision, and environmental simulation.
- **Gestures:** reusable embodied interaction and movement semantics.
- **The Art of Resupply:** general logistics concepts; this book supplies domain requirements and cases.
- **Host/runtime:** persistence, networking, device integration, execution, and user-facing orchestration.
- **SURPS:** optional shared identity, LOD, descriptor, and composition conventions.

This sourcebook does not claim that orbital simulation, navigation, spacecraft control, rendezvous automation, life support, or space logistics have been implemented. It does not provide operational maneuver instructions, targeting procedures, or methods for harming real space systems.

## 25. Glossary and research checklist

**Formation:** a mission-bounded set of assets, people, and support relationships with explicit roles and authority.

**Epoch:** the time associated with a state estimate or prediction.

**Reference frame:** the coordinate convention used to describe position, velocity, and orientation.

**Rendezvous:** a coordinated process of bringing objects into a defined relative state; not equivalent to visual proximity.

**Station keeping:** maintaining a required relationship or bounded state over time, subject to physical and resource constraints.

**Conjunction:** a predicted close passage between objects, with uncertainty that must be represented.

**Dual-use:** a system or service with both civilian and military applications or users.

**Graceful degradation:** retaining safe, reduced capability when part of a system or support network fails.

Before adding a case study, record its evidence label, epoch and reference frame, asset roles, crew and operator identities, command authority, communications assumptions, resource dependencies, support network, legal context, civilian consequences, and unresolved uncertainties.

## Cross-domain ownership and implementation boundary

Formations VI makes orbital and space formations a distinct member of the formation family. It does not treat spacecraft as ships or orbital trajectories as roads. Orbital Warfare and Space Infrastructure remains the broader security and governance volume; Interstellar Logistics remains a separate speculative scale. Physics owns physical simulation, raWWar owns domain meaning and scenarios, Gestures owns reusable embodied interaction, and SURPS composition remains opt-in.

## Related planetary expeditionary doctrine

- [Planetary Assault and Expeditionary Doctrine](planetary-assault-and-expeditionary-doctrine.md) — the expeditionary transition from orbital presence to planetary reception, sustainment, governance, and surface operations.
