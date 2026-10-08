# raWWar — Resource Extraction and Keyed Logistics

Status: Living design model.

> **Everything the civilization builds ultimately comes from somewhere. Resources are not an inventory abstraction; they are physical flows through an industrial network.**

## 1. Resource-first economy

The industrial system begins with resources.

Conceptual chain:

`Resource Deposit → Extraction → Raw Resource Storage → Raw-Resource Logistics → Processing/Refining → Refined Resource Storage → Refined-Resource Logistics → Factory/Facility → Product → Deployment → Operation`

Every stage consumes real capacity.

The resource itself is a typed capability/data identity.

Examples might eventually include:

- metals;
- silicates;
- carbon-bearing material;
- water;
- hydrocarbons;
- rare elements;
- radioactive materials;
- biological feedstock;
- faction-specific or world-specific resources;
- advanced resources discovered through research.

The exact resource catalogue remains content data.

## 2. Resource deposits

A deposit should record:

- resource identity;
- location;
- estimated quantity;
- accessible quantity;
- concentration/quality;
- extraction method;
- extraction technology requirement;
- environmental constraints;
- extraction throughput;
- power requirement;
- workforce requirement;
- maintenance requirement;
- infrastructure requirements;
- depletion/renewal behavior;
- ownership/control;
- known/unknown status.

A resource may exist in the world without the player initially knowing that it exists.

Research, exploration, sensors, surveying, and local knowledge can reveal deposits.

## 3. Resource-specific refinery tooling

A refinery is configurable.

The player can configure a refinery for a specific raw resource or resource family.

Example:

`Refinery Bay → Tooling → Resource Key → Extraction/Processing Capability`

When the player keys a refinery bay to a resource, the facility knows:

- what resource it accepts;
- what extraction technology it deploys/supports;
- what raw resource it stores;
- what processing procedure it runs;
- what refined output it produces;
- what inputs it consumes;
- what power it requires;
- what workers/qualifications it requires;
- what maintenance it requires;
- what logistics interfaces it exposes.

This is not a cosmetic label.

The key determines what physical work the facility is prepared to perform.

## 4. The refinery holographic surface

When laying out a base, the player should interact with the refinery as a physical/semantic object.

The holographic refinery surface can expose resource keys as selectable controls.

Conceptually:

`[Resource A] [Resource B] [Resource C] [Resource D] ...`

Selecting a resource does not magically complete the industrial chain.

It tells the organization:

> **This bay is intended to become a facility capable of handling this resource.**

The system then determines what is required.

The player can see:

- required tooling;
- required extraction technology;
- required power;
- storage;
- throughput;
- required supply vehicles;
- workforce;
- qualification;
- construction cost;
- operating cost;
- projected production;
- dependencies.

## 5. Keyed supply vehicles

Logistics vehicles are also resource-keyed.

A raw-resource supply vehicle can be configured specifically for:

`Raw Resource X → Raw Resource X Transport`

A refined-resource supply vehicle can be configured for:

`Refined Resource X → Refined Resource X Transport`

The vehicle therefore carries a semantic cargo identity in addition to its physical cargo capacity.

A keyed vehicle should know:

- accepted resource identity;
- cargo form;
- storage requirements;
- loading interface;
- unloading interface;
- compatible facilities;
- route requirements;
- operating cost;
- qualification requirements;
- maintenance requirements.

This prevents the logistics system from becoming "any truck can carry anything."

Some vehicles may intentionally remain multi-resource platforms when the technology and configuration support it.

## 6. Base layout provides intent

The player should not have to manually script every future logistics action.

While laying out a base, the player can establish intended resource controls:

`Refinery Bay → Resource Key → Storage → Supply Vehicle Requirement → Destination`

The organization then works out the operational details.

The player is defining the capability envelope.

The FSM system executes the procedures.

## 7. Automatic orchestration

Once the player establishes the intended configuration, the organization can automatically attempt to complete the chain.

Conceptual process:

`Resource Key Selected`
→ determine required extraction technology
→ determine refinery tooling
→ determine storage
→ determine loading/unloading interfaces
→ determine supply vehicle configuration
→ determine receiving facility
→ determine production requirements
→ determine staffing/qualification
→ determine logistics route
→ schedule work
→ commission capability
→ begin operation

Nothing should appear from nowhere.

Every missing dependency becomes an observable requirement or bottleneck.

## 8. The ten-second organizational decision window

After the player establishes the intent, the organization gets a short decision window before committing available industrial capacity.

A conceptual example is:

1. Player keys a resource.
2. System evaluates available production capacity.
3. Organization waits briefly for existing queued work to be considered.
4. An available factory bay is selected if appropriate.
5. Otherwise an underutilized/redundant bay may be reconfigured.
6. Required tooling and production work are scheduled.
7. Supply vehicle production is queued.
8. Staffing/training and logistics are scheduled.

The exact duration should remain tunable simulation data, not a hard-coded UI trick.

The point is to model organizational reaction time.

## 9. Factory bay use expectations

A production bay can have an intended use expectation.

Examples:

- permanent production;
- preferred production family;
- surge production;
- reserve;
- maintenance;
- flexible/unspecialized;
- emergency conversion.

This lets the organization distinguish:

> "This bay is currently idle"

from:

> "This bay is deliberately reserved for a capability we expect to need."

Rekeying a bay can therefore have a cost.

The cost may include:

- tooling change;
- calibration;
- cleaning/decontamination;
- worker reassignment;
- training;
- quality-control setup;
- material handling changes;
- downtime;
- lost production;
- future restart cost.

## 10. Player attention is a resource

The player can intervene before the organization automatically decides.

For example:

> **Assign Bay 4 to raw iron transport vehicle production now.**

This can produce a faster result because the player has removed organizational uncertainty and scheduling delay.

The benefit is not a magical bonus.

It is the consequence of:

- earlier commitment;
- reduced queueing;
- reduced decision delay;
- deliberate tooling;
- planned staffing;
- planned logistics.

The player is rewarded for understanding the industrial system.

## 11. Automatic vs deliberate organization

The system should support both.

### Automatic

Player expresses capability intent.

Advisors and FSMs determine the best currently available execution path.

### Deliberate

Player explicitly assigns:

- factory bay;
- tooling;
- workforce;
- supply vehicles;
- routes;
- storage;
- priorities;
- production sequence.

Deliberate intervention can outperform automatic organization when the player has better information or accepts costs the automatic system would avoid.

## 12. Raw and refined logistics

A resource should have a chain of custody.

Example:

`Deposit`
→ extraction
→ raw storage
→ raw-resource supply vehicle
→ processing facility
→ refined storage
→ refined-resource supply vehicle
→ consuming factory
→ finished component
→ finished-goods logistics
→ deployment.

Each transfer has:

- source;
- destination;
- resource identity;
- quantity;
- physical container/form;
- loading procedure;
- transport;
- unloading procedure;
- route;
- time;
- loss/spoilage where applicable;
- receiving capacity.

This makes logistics observable and debuggable.

## 13. Resource-specific infrastructure

A resource may require dedicated infrastructure.

Examples:

- extraction machinery;
- pumps;
- drills;
- conveyors;
- pressure systems;
- tanks;
- silos;
- cooling;
- radiation shielding;
- chemical processing;
- hazardous-material handling;
- loading equipment;
- specialized vehicles.

Therefore:

> **A resource key can change the physical design of the facility.**

The refinery is not a generic building with a dropdown.

## 14. Resource discovery and research

The player may discover a resource before knowing how to exploit it.

Possible progression:

`Resource discovered`
→ survey
→ extraction research
→ extraction technology
→ refinery process research
→ tooling
→ facility configuration
→ supply vehicle configuration
→ production
→ operational extraction
→ refined output
→ industrial integration.

Research therefore changes what the civilization can physically do with the world.

## 15. Resource bottlenecks

The economy should naturally expose bottlenecks.

A player may have:

- enormous deposits but insufficient extraction;
- extraction but insufficient raw-resource transport;
- raw material but insufficient refinery capacity;
- refined material but insufficient refined-resource transport;
- factories but insufficient refined inputs;
- production capacity but insufficient qualified workers;
- resources and factories but insufficient power;
- everything except maintenance capacity.

The simulation should expose the actual bottleneck rather than simply displaying "resource unavailable."

## 16. FSM composition

The resource network can be composed from procedures such as:

- Deposit Discovery FSM;
- Survey FSM;
- Extraction FSM;
- Raw Storage FSM;
- Loading FSM;
- Resource Transport FSM;
- Unloading FSM;
- Refinery Configuration FSM;
- Processing FSM;
- Refined Storage FSM;
- Refined Transport FSM;
- Factory Consumption FSM;
- Production FSM;
- Maintenance FSM;
- Qualification FSM;
- Logistics Scheduling FSM;
- Facility Reconfiguration FSM.

The centralized FSM implementation is what allows these independent procedures to coordinate.

## 17. Commander-facing view

The commander should not have to understand the entire graph to use it.

The surface view can say:

**Iron extraction**
- Resource: Iron
- Raw throughput: 500 units/day
- Refined output: 320 units/day
- Current bottleneck: raw transport
- Required: 2 keyed raw-resource vehicles
- Current: 1
- ETA to full throughput: 2 days

The deep dive can explain why.

The commander sees the consequence first.

## 18. Strategic consequence

The most powerful player behavior is not clicking faster.

It is understanding the civilization well enough to make decisions before the bottleneck appears.

A player who lays out:

- extraction sites;
- refinery bays;
- storage;
- keyed supply vehicles;
- factory capacity;
- transport corridors;
- power infrastructure;
- workforce;
- maintenance;

in advance should be materially more effective than a player who waits for shortages and reacts.

That is the reward for attention to detail.

> **The player designs the capability. The organization builds it. The logistics network keeps it alive.**
