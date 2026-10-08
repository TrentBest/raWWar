# raWWar — Scout Vehicle Cut Sheet

Status: Living design model. Initial role definition.

> **The scout is a soldier's mobility system first. The sensor network eventually changes what "scouting" means.**

## 1. Role

The scout vehicle exists to move a qualified soldier rapidly through uncertain territory while exposing as little combat mass as practical.

Its intended characteristics are:

- very high mobility;
- low mass;
- light protection;
- minimal direct armament;
- strong observation capability;
- strong communications;
- low operating footprint;
- small crew;
- rapid deployment;
- rapid withdrawal.

It is not intended to win prolonged fights against dedicated combat vehicles.

## 2. Default faction configuration

The faction default should define the baseline scout configuration.

The exact hardware remains faction/content data.

Baseline concept:

| Category | Default |
|---|---|
| Chassis | Lightweight high-mobility scout chassis |
| Locomotion | High-speed terrain-capable system |
| Power | Lightweight high-output source |
| Protection | Light |
| Armament | Minimal/self-defense |
| Sensors | Short/medium-range active and passive observation |
| Communications | Strong relative to vehicle mass |
| Crew | Scout/operator plus optional second position |
| Cargo | Minimal mission equipment |
| Doctrine | Observe, report, relocate |

## 3. Chassis

The scout chassis prioritizes:

- acceleration;
- top speed;
- maneuverability;
- terrain handling;
- low mass;
- power-to-mass ratio;
- sensor mounting;
- communications;
- low silhouette where practical.

The chassis sacrifices:

- armor mass;
- heavy weapon capacity;
- large crew;
- large cargo volume.

Those tradeoffs are physical configuration consequences.

## 4. Crew positions

### Scout/operator

Primary responsibilities:

- drive/maneuver;
- navigation;
- observation;
- sensor operation;
- communication;
- reconnaissance procedure;
- threat reporting;
- route selection;
- emergency withdrawal;
- vehicle recovery procedure.

The position requires qualification in the vehicle's movement, observation, communication, and emergency systems.

### Optional sensor/communications operator

Where the configuration supports a second soldier, the second position can specialize in:

- sensor management;
- signal analysis;
- communications;
- target/report correlation;
- electronic observation;
- navigation assistance.

The faction may choose a one-person or two-person configuration depending on doctrine and technology.

## 5. Position data graph

Conceptually:

```
Optical / thermal / radar sensors
             ↓
       Sensor controller
             ↓
        Vehicle data bus
          ↙       ↘
 Scout station   Comms station
       ↓              ↓
 Qualified soldier → Network
```

The vehicle should not give the soldier omniscient knowledge.

The soldier receives the information that the installed sensors, data links, position, environment, and qualification make available.

## 6. Qualification

Minimum qualification should cover:

- vehicle operation;
- navigation;
- sensor operation;
- communication procedure;
- reconnaissance reporting;
- terrain assessment;
- emergency vehicle procedure;
- basic field maintenance;
- recovery/evacuation procedure.

Advanced qualification may unlock:

- specialized sensor operation;
- electronic reconnaissance;
- advanced navigation;
- sensor-network management;
- command reconnaissance;
- autonomous sensor deployment.

Qualification is a property of the soldier and position, not merely the vehicle.

## 7. Training pipeline

A new scout does not become qualified instantly.

Conceptual chain:

```
Candidate
  ↓
Basic soldier qualification
  ↓
Vehicle systems training
  ↓
Driving/mobility training
  ↓
Sensor training
  ↓
Communications training
  ↓
Reconnaissance procedure
  ↓
Supervised field exercises
  ↓
Qualification evaluation
  ↓
Operational scout
```

A soldier can therefore be physically present in a scout while remaining unqualified for one or more advanced positions.

## 8. Commander-facing statistics

The commander should primarily see consequences:

| Statistic | Meaning |
|---|---|
| Speed | How quickly the scout can reposition |
| Range | How far it can operate before logistics intervention |
| Survivability | Probability of surviving hostile exposure |
| Detection range | How far installed sensors can observe |
| Communication range | How far reports can propagate directly |
| Operating cost | Ongoing resource burden |
| Production cost | Cost to build |
| Crew burden | Personnel required |
| Training burden | Time/resources required to qualify crew |
| Sensor coverage | Information contribution to the force |
| Readiness | Ability to perform the mission now |

The commander can inspect the deep dive to discover why those numbers changed.

## 9. Configuration choices

Examples:

### Extended fuel system

- higher vehicle cost;
- higher mass;
- greater range;
- potentially reduced payload margin.

### Improved sensor suite

- higher vehicle cost;
- greater detection capability;
- higher power/cooling demand;
- greater operator qualification burden.

### Improved communications

- higher cost;
- greater network reach;
- increased power demand;
- potentially increased signature.

### Additional armor

- higher cost;
- improved survivability;
- greater mass;
- reduced acceleration/range;
- potentially increased maintenance burden.

### Minimal combat package

- lower direct combat vulnerability;
- small increase in self-defense capability;
- should not turn the scout into a combat vehicle.

The important design principle is that the commander sees the trade.

## 10. Scout → scan transition

The most important progression is not simply making the scout vehicle faster.

Eventually, the organization may research and field persistent detection systems.

Conceptual progression:

```
Mobile scout
    ↓
Improved mobile sensors
    ↓
Networked scout vehicles
    ↓
Deployable detector nodes
    ↓
Fixed sensor network
    ↓
Persistent map-scale detection
```

A detector node has:

- installation location;
- sensor type;
- power source;
- communications connection;
- coverage volume;
- detection limitations;
- maintenance requirement;
- vulnerability;
- qualification requirement.

Once enough nodes exist and the network is connected, the commander can see a large portion of the map without physically sending scouts everywhere.

This changes the meaning of the scout.

The scout remains valuable for:

- investigating ambiguous contacts;
- penetrating areas where fixed sensors cannot see;
- confirming intelligence;
- mobile reconnaissance;
- discovering changes;
- operating beyond network coverage;
- inspecting terrain and infrastructure;
- identifying targets that require human interpretation.

The technology does not make the soldier obsolete. It changes the soldier's job.

## 11. FSM composition

The scout should be composed from independent procedures for:

- driving;
- navigation;
- sensor operation;
- communications;
- observation;
- reporting;
- threat response;
- withdrawal;
- maintenance;
- refueling/recharging;
- recovery;
- qualification;
- mission assignment.

A scout mission therefore becomes an orchestration of soldier and vehicle FSMs rather than a scripted "scout animation."

## 12. Strategic consequence

A scout is cheap information mobility.

A sensor network is expensive persistent information infrastructure.

The commander chooses where the organization should spend resources.

The player should eventually be able to look at a map and understand:

> **We no longer need to send a soldier there just to find out what is happening.**

That is the payoff of research, production, deployment, logistics, qualification, and infrastructure working together.
