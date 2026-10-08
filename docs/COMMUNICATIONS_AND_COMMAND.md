# raWWar — Communications, Command, and Battle Planning

Status: Living design model.

> **The player gives intent through a command system. The world carries that intent through real people, procedures, communications, and time.**

## 1. Command is not unit clicking

The player's command experience should not be:

    Select unit → click destination → unit instantly knows what to do

It should instead be:

    Observe the world
      ↓
    Understand available information
      ↓
    Draw / configure a battle plan
      ↓
    Assign objectives, routes, areas, priorities, and timing
      ↓
    Transmit the plan
      ↓
    Relay through the communications network
      ↓
    Reach subordinate command
      ↓
    Interpret / acknowledge
      ↓
    Execute procedures
      ↓
    Report results
      ↓
    Update the player's understanding

The player's holographic war table is therefore a command instrument, not a conventional RTS unit-control panel.

## 2. The holographic war table

The commander can use a physical war table that projects the relevant portion of the world:

- base layout;
- terrain;
- known and suspected enemy positions;
- friendly formations;
- facilities;
- roads and movement infrastructure;
- logistics routes;
- sensor coverage;
- communication coverage;
- objectives;
- planned landing/drop zones;
- movement corridors;
- defensive areas;
- artillery/fire-support areas;
- known hazards;
- information age/confidence.

The table should expose what the commander can actually know.

It should not magically reveal the authoritative world.

## 3. Battle plans are intent

A battle plan can contain:

- objective locations;
- staging areas;
- movement corridors;
- drop/landing zones;
- formation routes;
- attack directions;
- defensive sectors;
- boundaries;
- priorities;
- timing;
- contingencies;
- rules of engagement where applicable;
- logistics requirements;
- communications requirements;
- reporting requirements.

The commander is expressing **what should happen and why**, while subordinate organizations determine the procedures required to accomplish it.

> **The player draws the plan. The organization performs the work.**

## 4. Plans travel through the chain of command

Orders are not instantaneous.

A plan may travel through:

    Commander
      ↓
    Command staff
      ↓
    Operations / communications
      ↓
    Regional command
      ↓
    Unit command
      ↓
    Squad / vehicle / facility
      ↓
    Individual procedure

Each stage can introduce:

- transmission delay;
- relay delay;
- queueing;
- acknowledgement delay;
- interpretation;
- loss of connectivity;
- degraded information;
- competing orders;
- changed circumstances;
- local discretion.

The exact chain depends on the organization's structure.

## 5. Communication is a physical capability

Communication requires infrastructure and resources:

- transmitters;
- receivers;
- antennas;
- relays;
- satellites/orbital infrastructure;
- power;
- qualified operators;
- encryption/security;
- network availability;
- maintenance;
- command authority.

A destroyed relay can therefore change the commander's effective reach.

A damaged antenna can create a communication shadow.

A busy network can delay lower-priority traffic.

A unit outside reliable coverage may have to operate under its last valid instructions and local doctrine.

## 6. Transmission time matters

The simulation should distinguish at least:

- transmission time;
- relay time;
- processing time;
- acknowledgement time;
- execution time.

A message that takes time to arrive can become obsolete before execution.

This is especially important across:

- large bases;
- planetary distances;
- orbital distances;
- star systems;
- fleets;
- warp preparation and travel;
- disrupted or damaged communication networks.

The player therefore learns to plan for uncertainty rather than expecting instantaneous control.

## 7. Information has age and confidence

A contact on the war table should have provenance.

Useful information state can include:

- observed time;
- source;
- relay path;
- estimated position;
- confidence;
- age;
- last confirmation;
- classification;
- whether it is direct observation, report, inference, or prediction.

The enemy base shown on the table may therefore be:

- directly observed;
- recently reported;
- estimated;
- stale;
- suspected;
- deliberately deceptive.

> **The war table shows what the commander knows, not what the simulation knows.**

## 8. Orders can become obsolete

Suppose the player draws:

    Unit A → advance here
    Unit B → secure this route
    Unit C → land here

By the time the order reaches the units:

- the enemy may have moved;
- the landing zone may be occupied;
- the route may be blocked;
- a relay may have failed;
- ammunition may have changed;
- another friendly unit may have arrived;
- weather may have changed;
- a subordinate commander may have discovered something new.

The organization must therefore have procedures for:

- continue;
- adapt;
- request clarification;
- report deviation;
- abort;
- replan;
- execute contingency.

This is where individual skill, doctrine, training, and command culture become meaningful.

## 9. The commander's advantage is information and intent

A player who personally examines the base, observes the terrain, studies the enemy, and builds a coherent plan should be able to outperform a player who merely issues isolated movement commands.

The advantage comes from:

- better information;
- better timing;
- better coordination;
- better priorities;
- better use of terrain;
- better logistics;
- better anticipation;
- better communication planning.

Not from an arbitrary commander damage multiplier.

## 10. Technology changes command

Technology can improve:

- transmission speed;
- relay capacity;
- range;
- resilience;
- encryption;
- sensor integration;
- map reconstruction;
- information freshness;
- communications security;
- autonomous relay behavior;
- battlefield visualization;
- command terminal capability.

Research should therefore expand the commander's capability envelope.

A faction with excellent communications technology may coordinate distant forces more effectively.

A faction with poor communications but strong local doctrine may be better at decentralized action.

A faction can deliberately research command resilience rather than raw weapons technology.

## 11. The chain of command is part of the simulation

A command structure is not merely organizational metadata.

It determines:

- who receives orders;
- who can interpret them;
- who can modify them;
- who can authorize action;
- who reports upward;
- who is responsible when communication fails;
- how quickly decisions propagate;
- how much local autonomy exists.

A strong commander with weak communications is still constrained.

A strong communications network with poor command procedures is still constrained.

> **Command capability emerges from people, procedure, infrastructure, information, and technology together.**

## 12. Battle planning is an Experience

The planning interface should itself be part of the world.

The player can physically:

- stand at the war table;
- inspect projected terrain;
- move around the projection;
- select a known formation;
- draw a route;
- designate a staging area;
- inspect logistics implications;
- establish a landing/drop zone;
- review expected communications coverage;
- assign priorities;
- issue the plan.

The interface can visually expose consequences before commitment.

For example:

    Planned route
      → terrain
      → road capacity
      → lane capacity
      → logistics reach
      → communication coverage
      → enemy exposure
      → estimated arrival
      → contingency options

The player is planning a military operation, not filling out a command menu.

## 13. Plans are persistent world objects

A battle plan should have:

- author;
- creation time;
- intended recipients;
- objectives;
- routes;
- constraints;
- priority;
- transmission history;
- acknowledgement history;
- revisions;
- execution state;
- deviations;
- outcome;
- lessons learned.

A plan can therefore become part of military history.

A failed plan should not simply disappear when the player closes a menu.

## 14. Command and FSM_API

The battle plan can be represented semantically and decomposed into procedures.

For example:

    Battle Plan
      ↓
    Objective
      ↓
    Unit Assignment
      ↓
    Movement Procedure
      ↓
    Navigation
      ↓
    Logistics
      ↓
    Communications
      ↓
    Engagement Procedure
      ↓
    Reporting
      ↓
    Outcome

Each stage can be driven by independent, interrelated FSMs.

The plan is intent.

The FSMs perform the work.

## 15. Global simulation and communications

Communications continue even when the player is elsewhere.

Remote systems can:

- send reports;
- relay orders;
- lose relays;
- repair networks;
- experience delays;
- discover events;
- change plans;
- fight without the player.

Event Horizons reduce detail, not causality.

A remote battle may be represented strategically until the player focuses on it, at which point the simulation expands into the relevant units, procedures, communications, and physical environment.

> **The player is not the source of the world's activity. The player is one participant in it.**
