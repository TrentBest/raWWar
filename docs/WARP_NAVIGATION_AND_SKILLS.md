# raWWar — Warp Navigation, Gravitational Hazards & Crew Skill

**Status:** Living design model.  
**Canon:** Warp travel takes substantial preparation time. A warp bubble cannot safely form or intersect a star's gravitational well.  
**Candidate:** Exact gravitational exclusion radii, navigation equations, failure probabilities, sensor requirements, and warp physics remain open.

> **Distance is not a loading screen. It is a problem the organization must solve.**

## 1. Why warp travel takes days

A starship cannot simply point at a destination and enter warp.

The ship must:

1. determine a viable route;
2. identify gravitational hazards;
3. construct a route around those hazards;
4. calculate the required warp path;
5. prepare the vessel;
6. align the navigation system;
7. initialize the warp bubble;
8. execute the jump.

This creates the intended strategic consequence:

**Warp travel takes days to prepare because safe navigation is difficult.**

## 2. Stars are hazards, not scenery

A warp bubble cannot safely form within a star's gravitational well.

More importantly, the bubble cannot safely intersect one during transit.

If a warp trajectory intersects a sufficiently hazardous stellar gravity well, the result is not "slow warp."

It is potentially catastrophic destruction of the ship.

Therefore the navigator must plan a path that bends around gravitational hazards.

Conceptually:

```
Origin ───────────────────────────── Destination
              ✕ STAR
             /             /              /        safe route
          /       ____________
```

The direct geometric path is not necessarily the fastest safe path.

## 3. Route planning is a skill problem

Every candidate route can contain a different number and severity of gravitational hazards.

The navigation challenge therefore depends on:

- number of stars near the intended path;
- gravitational influence/exclusion region;
- distance;
- route complexity;
- sensor quality;
- navigation system quality;
- crew skill;
- available preparation time;
- accumulated navigation experience;
- damage/condition of navigation equipment.

The ship does not simply have a single "warp speed" value.

It has a **safe route problem**.

## 4. Crew skill matters

A capable navigation system helps.

An experienced crew helps.

Neither completely replaces the other.

A candidate relationship is:

```
Route complexity
        ↓
navigation workload
        ↓
system capability + crew skill
        ↓
safe route confidence
        ↓
preparation time / risk
```

A novice crew operating an excellent system may require more preparation.

An expert crew can extract more value from the same equipment.

A poor system can constrain even an expert crew.

## 5. Navigation is not a single skill

Eventually, navigation should be represented as multiple related capabilities.

Candidate skills include:

- stellar navigation;
- gravitational navigation;
- route planning;
- sensor interpretation;
- astrogation;
- emergency navigation;
- formation navigation;
- long-range navigation;
- warp preparation;
- navigation-system operation.

The exact skill taxonomy remains open.

## 6. The route can change

The galaxy is alive.

A route planned yesterday may not be the route used tomorrow because:

- a fleet moved;
- a battle changed local conditions;
- a navigation hazard was discovered;
- a system became politically inaccessible;
- a ship was damaged;
- intelligence changed;
- another faction established an interdiction;
- the destination changed;
- the player changed the objective.

A navigation plan is therefore a procedure over current world state, not a permanent coordinate lookup.

## 7. Navigation creates strategic geography

If stars are dangerous to warp through, the shape of the galaxy matters.

Some systems become:

- natural hubs;
- chokepoints;
- safe corridors;
- dangerous detours;
- isolated regions;
- strategic staging areas.

A faction controlling a system near a difficult navigation corridor may possess strategic power even if the system itself is not resource-rich.

This gives the galaxy topology military meaning.

## 8. Individual soldiers are persistent skill-bearing entities

The same model applies below the ship level.

A soldier should not be:

```
"Infantry Unit #27"
```

They should be an individual with a history.

Candidate individual state:

- identity;
- attributes;
- skills;
- qualifications;
- rank;
- assignments;
- relationships;
- equipment;
- condition;
- experience;
- awards;
- procedures learned;
- combat history.

The exact attribute system remains open.

## 9. GURPS-like philosophy, not GURPS dependency

The useful idea is:

> **An individual has many skills that describe what that individual is capable of doing.**

We do not need to reproduce GURPS rules.

The Workshop should own the representation.

A skill can have:

- current level;
- experience evidence;
- training history;
- relevant contexts;
- specialization;
- decay/maintenance rules if later required;
- dependencies;
- certification relationship.

## 10. Experience comes from doing

A soldier who repeatedly performs an activity should become better at that activity.

Examples:

| Activity | Possible experience |
|---|---|
| live fire | weapons handling, marksmanship, tactical judgment |
| defending a position | defensive tactics, threat recognition, coordination |
| attacking | assault tactics, movement under fire, target recognition |
| vehicle repair | mechanical skill, diagnostics, field repair |
| flying | flight skill, navigation, emergency response |
| commanding | command, communication, tactical judgment |
| reconnaissance | observation, stealth, reporting |
| medical treatment | relevant medical skills |
| construction | relevant construction/engineering skills |
| logistics | supply handling, routing, inventory procedures |
| research | laboratory/research skills |

The exact mapping should be data-driven.

## 11. Training and experience are different

Training can provide controlled repetition.

Experience provides consequences.

A VR training module can teach a soldier to shoot.

Actual combat teaches the soldier what it means to shoot while:

- exhausted;
- frightened;
- wounded;
- under return fire;
- protecting another person;
- operating with incomplete information.

Therefore:

> **Training creates capability. Experience creates depth.**

A veteran and a newly qualified soldier can have the same qualification while having materially different experience histories.

## 12. Experience should be attributable

The simulation should eventually know:

- what the soldier attempted;
- what skill was relevant;
- whether the attempt succeeded;
- how difficult the situation was;
- whether the soldier was under pressure;
- whether the soldier learned something;
- whether repetition was meaningful.

This does not mean every trigger needs to award visible experience points.

The authoritative simulation can record meaningful events and derive progression from them.

## 13. Skill, qualification, permission, readiness

These must remain separate:

```
Skill
  = what the individual can do

Qualification
  = what the organization recognizes the individual as trained/certified to do

Permission
  = what the organization authorizes the individual to do

Readiness
  = whether the individual can perform it now
```

A brilliant fighter pilot can be grounded.

A qualified mechanic can be exhausted.

An experienced soldier can lack permission to access a restricted system.

A newly trained soldier can be qualified but inexperienced.

This distinction is essential to making the personnel simulation believable.

## 14. Skill affects the rest of the simulation

Individual skill should eventually influence:

- task duration;
- error probability;
- recovery;
- equipment utilization;
- route planning;
- maintenance quality;
- combat performance;
- command effectiveness;
- training speed;
- reaction time;
- decision quality;
- procedure reliability.

But skill should not replace procedure.

The FSM defines what is supposed to happen.

The individual's skill affects how well the individual executes the procedure within the legal state space.

## 15. The same principle applies at every scale

```
Galaxy
  → faction
    → sub-faction
      → organization
        → squad
          → individual
```

At each level:

**capability + resources + people + skills + procedures + history = actual behavior**

This is how the galaxy can be enormous without becoming arbitrary.

> **The world is data. The people have history. The FSMs perform the work.**
