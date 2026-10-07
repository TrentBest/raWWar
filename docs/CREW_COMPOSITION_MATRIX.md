# raWWar — Crew Composition Matrix

Status: Living design model.

> A capability is operational when the required people, qualifications, equipment, readiness, and procedure can be assembled.

## Crew vocabulary

Person = individual identity, qualifications, condition, equipment, assignment, and history.

Squad = four people.

Crew = people required to operate one capability.

Specialty = required role/qualification family.

Shift = personnel assigned during a period.

Reserve = qualified personnel available to replace unavailable crew.

## Initial matrix

| Capability | Crew shape | Training implication | Status |
|---|---|---|---|
| Light utility vehicle | Small vehicle crew | Ground vehicle qualification | Candidate |
| Cargo carrier | Driver + cargo/logistics | Vehicle + logistics | Candidate |
| APC | Driver + vehicle crew + embarked soldiers | Armored + infantry | Candidate |
| IFV | Driver + gunner + commander + support | Armored + weapons + leadership | Candidate |
| Main battle tank | Dedicated armored crew | Driver/gunner/commander path | Candidate |
| Artillery | Artillery crew | Crew-served + artillery | Candidate |
| Heavy VTOL | Flight + loading/deployment crew | Pilot + cargo + loading | Candidate |
| Vehicle transport VTOL | Flight + cargo/deployment crew | Pilot + loading + logistics | Candidate |
| Fighter | Specialized flight crew | Pilot + maintenance support | Candidate |
| Recon VTOL | Flight + sensor/recon role | Pilot + reconnaissance/sensors | Candidate |
| EW aircraft | Flight + EW specialists | Pilot + electronic warfare | Candidate |
| Research facility | Researchers + technicians + administrator support | Research + laboratory + administration | Counts Candidate |
| Materials factory | Operators + technicians + maintenance + QC | Factory + materials | Candidate |
| Vehicle factory | Assembly + fabrication + engineering + QC + maintenance | Multiple technical families | Candidate |
| Aerospace factory | Aerospace + propulsion + systems + QC | Advanced technical families | Candidate |
| Command center | Command + communications + intelligence | Leadership + communications + intelligence | Candidate |
| Construction site | Construction + specialty crews + equipment operators | Construction ladder | Candidate |

## The important data shape

A capability should eventually resolve:

Capability → Crew Requirements → Specialty + Quantity → Minimum Qualification → Equipment → Facility → Readiness → Procedure.

Storing only CrewSize is insufficient. Two crews of eight may have completely different specialties and therefore completely different capabilities.

## Squad bottleneck

If a capability requires multiple squads of trained personnel, the player must provide corresponding training throughput.

The underlying requirement is training capacity, not an arbitrary rule that every crew must occupy an identical barracks.

## Factory crews

Materials factories likely need materials technicians, power/utilities, quality assurance, maintenance, logistics, and supervision.

Vehicle factories likely need assembly, fabrication, systems integration, electronics, propulsion/power, quality assurance, maintenance, and logistics.

Aerospace factories likely add aerospace assembly, avionics/control, propulsion, structural/armor, sensors, quality assurance, maintenance, and logistics.

Exact counts and shifts remain Illumination Needed.

## Research facilities

Known structure:

Research Facility + HQ Administrator Office + researchers/technicians + research equipment + resources + demonstration procedure → Research capability.

The player can visit the underground HQ office, discuss research with the administrator, schedule a demonstration, and visit the facility to observe it.

## Construction crews

Construction is assembled around the work rather than a universal builder unit.

Large work may require general construction, structural specialists, power/utilities, heavy equipment operators, material specialists, survey/site personnel, supervision, safety/security, and logistics.

## Readiness and shifts

Continuous capabilities must distinguish qualified people from people actually available for duty.

Future crew state should include on-duty requirement, reserve requirement, shift, fatigue/condition, equipment availability, maintenance state, and assignment conflicts.

## Open illumination

Exact crew counts, shift ratios, command relationships, reserves, cross-training, faction doctrine, training time, and minimum qualification tiers remain unresolved.
