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

Commander intent → requirement → inventory → transport → qualified personnel → procedure → capability → observed result

A missile tank therefore does not receive missiles because the Commander clicked a button. The organization must have missiles, storage, transport, loading capability, qualified personnel, and an applicable procedure.

## 3. Depletion is real

Consumables are finite: tank ammunition, missiles, aircraft stores, fuel/energy, construction materials, factory inputs, spare parts, and medical supplies.

When inventory reaches zero, dependent work cannot continue as though inventory still existed.

The dependent procedure can wait, pause, reduce output, change task, request resupply, abandon the current attempt, or use an alternative if one exists. Exact behavior belongs to the capability/procedure.

## 4. Do not require exact-resource waiting

The player should not be forced into: I want to build this, but I cannot click it until I have exactly enough resources.

Instead, intent can be expressed when the organization has partial capacity. Construction can begin when prerequisites allow it, consume available resources according to its procedure, and wait when required resources are unavailable.

This produces the strategic feeling of resource depletion without requiring a microscopic resource-counting interface.

## 5. Dune-like depletion behavior

The desired direction is closer to the useful part of classic Dune 2 resource behavior: when the source of a resource is exhausted, dependent production naturally stops or waits.

The important difference is that raWWar models the physical chain underneath the abstraction.

A depleted resource field can therefore cause: resource processor waiting → factory input shortage → production waiting → logistics demand → strategic consequence.

## 6. StarCraft-style micro is not the goal

Exact resource gating can create unnecessary actions: waiting for an exact threshold, repeatedly checking a resource number, issuing the same transfer manually, or moving individual inventory items that an organization should already understand.

The meaningful gameplay is deciding what matters, where it matters, how much priority it receives, what trade-offs are acceptable, which capability should receive scarce resources, and when to intervene personally.

## 7. Priority is gameplay

If there are three fronts and enough ammunition to fully support only two, the player has a meaningful decision. They may prioritize the invasion, preserve a defensive line, accept reduced anti-air coverage, redirect factory production, move stockpiles, request additional transport, or personally intervene.

The logistics machinery performs the resulting work.

## 8. Buildings as keyed capabilities

Many structures have an authoritative capability key.

Examples:

- processing plant → resource/process;
- storage → inventory category;
- barracks → training program;
- factory → production program;
- research facility → research domain/program.

The key changes what the structure is for without pretending that all structures are interchangeable.

## 9. Re-keying

Compatible structures can be re-keyed rather than demolished.

Current Candidate rule:

- re-keying time = one-quarter of original construction time;
- re-keying cost = twice the normal construction cost.

The conversion can still require tooling, equipment changes, inventory movement, staff reassignment, training, qualification changes, and configuration procedures.

Therefore re-keying is faster than rebuilding, but it is not free or magical.

## 10. FPS consequence

The Soldier can encounter the consequences of Commander intent.

If the Commander prioritizes missile production, factory crews work, materials move, missiles appear in storage, logistics transports them, loading crews replenish launchers, and soldiers see the result.

If the Commander neglects that priority, storage stays empty, launchers become depleted, crews wait, and soldiers experience the shortage.

The two perspectives therefore reinforce one another.

## 11. Design principle

> **Abstract the bookkeeping, never abstract away the consequence.**

The player should not need to count every missile.

The world should absolutely know when there are no missiles.
