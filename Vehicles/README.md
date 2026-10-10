# raWWar Vehicles

This directory is the working vehicle design laboratory.

A vehicle is a physical capability with a chassis, configuration, crew, ammunition or consumables, maintenance requirements, production lineage, qualifications, procedures, and consequences.

Individual Markdown records remain the human-readable design laboratory. The populated seed corpus in `data/vehicles.json` is the first machine-readable content layer and uses stable semantic IDs shared with resources, technologies, facilities and cut sheets.

## Vehicle capability model

`Chassis → Sockets → Components → Configuration → Capability → Qualified Crew → Readiness`

The same chassis can produce different vehicles through configuration.

A vehicle record therefore answers:

- What physical chassis exists?
- Which sockets are available?
- Which components occupy them?
- What power, mobility and protection result?
- Which sensors, weapons, tools and mission systems are installed?
- Which crew positions exist?
- What qualifications does each position require?
- What does the vehicle consume?
- What maintenance keeps it ready?
- What research enables the configuration?
- What production and logistics are required?
- What happens when a dependency fails?

## Populated seed corpus

The current seed records include:

- Sparrow Scout;
- Bulwark APC;
- Jackal Combat Quad;
- Lancer Missile Tank;
- Kestrel Recon VTOL;
- Atlas Heavy Carryall;
- Mantis Ornithopter Alpha;
- Vanguard Starfighter;
- Resolute Heavy Cruiser;
- Imperial Fleet Carrier.

These are not intended to be a final balanced roster. They are interconnected content records designed to exercise the simulation model across ground, atmospheric and space environments.

## Commander cut sheets

Every populated vehicle can project a commander-facing cut sheet.

The commander sees consequences first:

- purpose;
- capacity;
- readiness;
- crew burden;
- logistics burden;
- power/fuel requirements;
- strengths;
- weaknesses;
- current failure conditions.

The underlying record remains available for deep dives into engineering, crew procedures, Gestures, maintenance, production and research.

## Core rule

A vehicle does not magically replenish what it consumes. A missile vehicle needs missiles. A damaged vehicle needs repair. A destroyed vehicle does not automatically erase surviving crew.

The same vehicle must make sense from both sides of play: Commander view sees capability, readiness, logistics and assignment; Soldier view sees the cockpit, crew stations, controls, procedures, danger, damage and survival.

## The important part

The vehicle records are deliberately **not the simulation**.

They describe what a vehicle is.

FSM-driven procedures determine what its crew does, what its systems do, what happens when something goes wrong, how long an action takes, what dependencies are waiting, and what history is created.

The data describes the machine.

The FSM makes the machine live.
