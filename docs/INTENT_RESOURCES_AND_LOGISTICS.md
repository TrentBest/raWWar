# raWWar — Intent, Resources, and Logistics

**Status:** Living systems model.

> **The player expresses intent. The organization performs the work.**

This model avoids two opposite failures: making resources magical and consequence-free, or making the player micromanage every crate, shell, missile, worker, and transfer.

## 1. Intent is not a resource transaction

The Commander should be able to say:

- keep this unit supplied;
- maintain anti-air coverage;
- build the next defensive position;
- prepare the invasion fleet;
- produce more Carryalls;
- train more pilots;
- keep this factory operating;
- reinforce this front.

Those statements become authoritative organizational intent. The simulation attempts to satisfy that intent through real capabilities.

## 2. The chain remains real

For example:

```
Commander intent
      ↓
requirement
      ↓
inventory
      ↓
transport
      ↓
qualified personnel
      ↓
procedure
      ↓
capability
      ↓
observed result
```

A missile tank therefore does not receive missiles because the Commander clicked a button. The organization must have missiles, storage, transport, loading capability, qualified personnel, and an applicable procedure.

## 3. Supply lines are real

A military force is not supplied by proximity to a resource number.

Supplies have to move through a physical/logical chain:

```
Production / acquisition
      ↓
Strategic stockpile
      ↓
Regional depot
      ↓
Forward logistics node
      ↓
Transport route
      ↓
Unit / vehicle / facility
      ↓
Consumption
```

The exact chain may be shorter or longer depending on geography and organization.

A soldier at the front therefore has a finite quantity of ammunition, food, medical supplies, equipment, and other consumables. A vehicle has finite ammunition, fuel/energy, operating stores, and practical range. Aircraft and spacecraft have their own stores, servicing, and readiness requirements.

The organization normally handles replenishment.

The player does not need to manually order a truck to carry every box.

But the world must know that the truck, route, inventory, personnel, and destination all exist.

## 4. Limited ammunition is authoritative

A soldier cannot fire ammunition that does not exist.

A vehicle cannot continue firing indefinitely because a UI counter was refilled.

A unit with insufficient ammunition can:

- request resupply;
- reduce expenditure;
- change mission;
- withdraw;
- use an alternative weapon if available;
- wait for supplies;
- continue with degraded combat capability.

The exact behavior belongs to the unit's procedures and doctrine.

The same principle applies to:

- missiles;
- tank rounds;
- aircraft stores;
- energy/fuel;
- medical supplies;
- spare parts;
- construction materials;
- factory inputs;
- food;
- specialized consumables.

> **The abstraction is the order, not the consequence.**

## 5. Range is real

Vehicles have physical operating limits.

A vehicle may have enough ammunition but still be unable to reach the destination because it lacks:

- fuel/energy;
- maintenance readiness;
- crew endurance;
- suitable route;
- transport support;
- environmental capability;
- required servicing;
- replacement crew.

Long-range operations therefore create logistics requirements before combat even begins.

A vehicle's stated range should not be treated as a magical movement radius. It is a consequence of its configuration, load, environment, operating profile, crew, and available support.

## 6. Supply lines can fail

A supply line can be:

- overloaded;
- delayed;
- rerouted;
- damaged;
- attacked;
- blocked;
- starved of vehicles;
- starved of qualified personnel;
- starved of fuel/energy;
- starved of inventory;
- made unsafe by weather or terrain;
- disrupted by enemy intelligence or sabotage.

When that happens, the consequence propagates forward.

```
Supply disruption
      ↓
forward inventory falls
      ↓
consumption continues
      ↓
readiness falls
      ↓
procedures change or pause
      ↓
combat / production / construction capability degrades
      ↓
strategic consequences
```

This is the physical meaning of a supply line.

## 7. The organization handles routine logistics

The Commander should normally be able to establish intent such as:

- keep First Battalion supplied;
- maintain the forward artillery group;
- keep the factory operating;
- maintain fighter readiness;
- stockpile missiles at this depot;
- support the invasion force;
- prioritize medical evacuation;
- preserve reserves for the defensive front.

Advisors, logistics staff, administrators, commanders, and subordinate organizations turn those priorities into actual work.

If the organization has enough capability and the player's advisors are properly supported, most routine logistics should happen without direct intervention.

That is not a cheat.

That is what a functioning military organization is supposed to do.

## 8. Advisors are a capability

Advisors are not decorative menu characters.

They are part of the organizational machinery that converts command intent into coordinated work.

A capable advisor can:

- monitor supply levels;
- identify shortages before they become critical;
- prioritize competing demands;
- allocate transport;
- redirect stockpiles;
- request production;
- establish reserves;
- identify bottlenecks;
- warn about failing routes;
- recommend operational changes;
- coordinate subordinate organizations.

Their effectiveness can depend on:

- qualification;
- experience;
- workload;
- information quality;
- available staff;
- communication;
- organizational support;
- resources;
- trust/authority;
- faction doctrine.

### Paying advisors

Advisors should be properly compensated and supported.

> **If the player underfunds the people responsible for running the organization, the organization should become less capable of running itself.**

Underfunding should not simply produce a UI penalty.

It can create real organizational consequences:

- slower response;
- poorer planning;
- missed shortages;
- weaker coordination;
- reduced reserve management;
- increased logistics inefficiency;
- slower recovery from disruption.

The exact compensation/effect curve remains Illumination Needed.

## 9. Player attention creates advantage

The organization handles routine work.

The player can personally intervene.

That intervention should matter.

> **The player does not have to micro-manage the war. But when the player does micro-manage something intelligently, the organization should become better at accomplishing it.**

Examples:

- personally inspecting a forward depot;
- manually prioritizing a critical ammunition shipment;
- personally directing a convoy around a threat;
- reallocating scarce missiles between fronts;
- visiting a factory and resolving a bottleneck;
- personally reviewing a logistics plan;
- ordering an emergency reserve;
- personally coordinating a high-risk resupply operation;
- taking direct command during a crisis.

These interventions can produce bonuses because the player is adding attention, information, timing, and decision quality.

The bonus should emerge from actual work rather than a generic “+10% productivity” button.

For example:

```
Player attention
      ↓
better information / priority / coordination
      ↓
less waiting / fewer conflicts / better routing
      ↓
higher effective throughput
      ↓
better readiness
```

The exact numerical benefit is system-dependent.

## 10. Activity should be broadly productive

A highly active Commander can therefore improve the organization across many fronts.

The effect should not be:

> click frequently = free experience.

Instead:

> **meaningful intervention = improved organizational performance.**

A player who spends time:

- training with soldiers;
- inspecting factories;
- reviewing research;
- visiting logistics nodes;
- commanding battles;
- resolving supply problems;
- speaking with advisors;
- personally coordinating operations;

can create more effective outcomes than a player who delegates everything.

This makes activity itself a strategic resource.

### The important balance

The game should support three valid play styles:

**Delegator**

The player establishes intent and trusts the organization.

Result: normal organizational performance.

**Manager**

The player frequently reviews and adjusts priorities.

Result: improved coordination and fewer avoidable failures.

**Active Commander**

The player personally participates in important work and crises.

Result: strongest potential performance, but also greater demand on the player's attention.

None of these should make the others invalid.

## 11. Micro should create leverage, not obligation

We should avoid turning logistics into an RTS chore list.

The player should not be required to:

- move every ammunition crate;
- click every truck;
- manually refill every vehicle;
- assign every warehouse worker;
- personally approve every routine shipment.

That would make the organization meaningless.

Instead, micro should be an opportunity to intervene where attention has unusually high value.

> **Delegation provides the baseline. Attention provides leverage.**

## 12. Priority is gameplay

If there are three fronts and enough ammunition to fully support only two, the player has a meaningful decision.

They may:

- prioritize the invasion;
- preserve a defensive line;
- accept reduced anti-air coverage;
- redirect factory production;
- move stockpiles;
- request additional transport;
- strengthen a forward depot;
- personally intervene.

The logistics machinery performs the resulting work.

This creates the desired relationship:

```
Strategy
  ↓
Priority
  ↓
Organization
  ↓
Logistics
  ↓
Physical work
  ↓
Readiness
  ↓
Combat capability
  ↓
Outcome
```

## 13. Depletion is real

Consumables are finite: tank ammunition, missiles, aircraft stores, fuel/energy, construction materials, factory inputs, spare parts, and medical supplies.

When inventory reaches zero, dependent work cannot continue as though inventory still existed.

The dependent procedure can wait, pause, reduce output, change task, request resupply, abandon the current attempt, or use an alternative if one exists. Exact behavior belongs to the capability/procedure.

## 14. Do not require exact-resource waiting

The player should not be forced into:

> I want to build this, but I cannot click it until I have exactly enough resources.

Instead, intent can be expressed when the organization has partial capacity. Construction can begin when prerequisites allow it, consume available resources according to its procedure, and wait when required resources are unavailable.

This produces the strategic feeling of resource depletion without requiring a microscopic resource-counting interface.

## 15. Dune-like depletion behavior

The desired direction is closer to the useful part of classic Dune 2 resource behavior: when the source of a resource is exhausted, dependent production naturally stops or waits.

The important difference is that raWWar models the physical chain underneath the abstraction.

A depleted resource field can therefore cause:

```
resource processor waiting
        ↓
factory input shortage
        ↓
production waiting
        ↓
logistics demand
        ↓
strategic consequence
```

## 16. StarCraft-style micro is not the goal

Exact resource gating can create unnecessary actions: waiting for an exact threshold, repeatedly checking a resource number, issuing the same transfer manually, or moving individual inventory items that an organization should already understand.

The meaningful gameplay is deciding what matters, where it matters, how much priority it receives, what trade-offs are acceptable, which capability should receive scarce resources, and when to intervene personally.

## 17. Buildings as keyed capabilities

Many structures have an authoritative capability key.

Examples:

- processing plant → resource/process;
- storage → inventory category;
- barracks → training program;
- factory → production program;
- research facility → research domain/program.

The key changes what the structure is for without pretending that all structures are interchangeable.

## 18. Re-keying

Compatible structures can be re-keyed rather than demolished.

Current Candidate rule:

- re-keying time = one-quarter of original construction time;
- re-keying cost = twice the normal construction cost.

The conversion can still require tooling, equipment changes, inventory movement, staff reassignment, training, qualification changes, and configuration procedures.

Therefore re-keying is faster than rebuilding, but it is not free or magical.

## 19. FPS consequence

The Soldier can encounter the consequences of Commander intent.

If the Commander prioritizes missile production, factory crews work, materials move, missiles appear in storage, logistics transports them, loading crews replenish launchers, and soldiers see the result.

If the Commander neglects that priority, storage stays empty, launchers become depleted, crews wait, and soldiers experience the shortage.

The two perspectives therefore reinforce one another.

## 20. Design principle

> **Abstract the bookkeeping, never abstract away the consequence.**

The player should not need to count every missile.

The world should absolutely know when there are no missiles.

And when the player personally intervenes, that intervention should be visible in the work:

> **The player is not a required clerk. The player is a source of leverage.**
