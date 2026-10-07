# raWWar — Deterministic Simulation and Research Timing

Status: Living systems model.

> **A frame is not a game state. A frame is an opportunity to execute work.**

## 1. The distinction

Many games implicitly treat the render/update frame as the universal clock:

    every frame
    → update everything
    → recalculate everything
    → render

raWWar does not need to work that way.

The Experience contains different kinds of state and different processes have different natural cadences.

The Renderer may run at 60, 90, 120, or another presentation rate.

That does not mean research, strategic planning, factory accounting, logistics, or every FSM needs to be recalculated at that rate.

## 2. Three useful concepts

### Frame

A frame is a presentation/execution opportunity.

It is appropriate for work that must respond continuously, such as:

- rendering;
- immediate player input;
- physical motion;
- collision-sensitive behavior;
- Gesture progression;
- near-field observation.

### Simulation state

A state is authoritative game truth.

Examples:

- building is planned;
- building is under construction;
- building is complete;
- research program has 43.7% progress;
- factory is producing a vehicle;
- squad is assigned to a training session;
- VTOL is flight-ready;
- invasion is staged.

A state persists until a valid transition changes it.

### Simulation phase/checkpoint

A phase/checkpoint is a scheduled opportunity to evaluate a class of state.

Examples:

- research update;
- production update;
- logistics update;
- strategic update;
- population update;
- maintenance update.

These do not need to occur every presentation frame.

## 3. FSM relationship

FSM_API remains responsible for stateful behavior.

An FSM transition does not imply that the FSM must run at the renderer's frame rate.

A process can be awakened when:

- an event occurs;
- a dependency changes;
- its scheduled evaluation point arrives;
- a participant becomes available;
- a timeout expires;
- external input occurs.

## 4. Research programs are deterministic

Research is not a random technology lottery.

A research program exists because the game has defined it.

The player decides how much research capacity to allocate to that program.

The program progresses deterministically toward 100.

No random roll decides whether the research succeeds.

## 5. Research allocation

Represent research allocation as a value:

    0 ≤ Allocation ≤ 100

Interpretation:

- 0% = no research capacity assigned;
- 10% = receives one tenth of maximum research throughput;
- 50% = receives half of maximum throughput;
- 100% = maximum research throughput.

The crucial point:

> **100% allocation means maximum speed, not instant completion.**

## 6. Time to completion

Let:

- Tmax = time required at 100% allocation;
- A = allocation fraction from 0.0 to 1.0;
- R = current research progress from 0.0 to 1.0.

For constant allocation above zero:

    time_remaining = (1 - R) × Tmax / A

Therefore:

- 100% → Tmax for a new program;
- 50% → 2 × Tmax;
- 10% → 10 × Tmax;
- 0% → no progress.

If allocation changes, remaining time changes according to the new throughput.

## 7. Research update cadence

Research can be evaluated at a scheduled research checkpoint rather than every render frame.

For example:

    Frame 0
    Frame 1
    ...
    Frame 19
    RESEARCH CHECKPOINT
    ...
    Frame 39
    RESEARCH CHECKPOINT

The exact cadence is an implementation experiment, not a game-design rule.

A checkpoint calculates elapsed simulation time since the previous research evaluation and advances every active program accordingly.

> **We do not slow research because we only checked it every 20 frames.**

We calculate elapsed time and apply the correct deterministic progress for that interval.

The cadence is an optimization and scheduling decision.

## 8. Event-driven alternative

A research program does not necessarily need periodic polling at all.

A future implementation could schedule its next meaningful evaluation based on:

- allocation change;
- expected completion time;
- dependency change;
- research phase boundary;
- player interaction.

The semantic state remains the same.

## 9. Research thresholds

The 0–100 value is continuous progress through a defined research program.

Thresholds can unlock new programs or branches.

Example:

    PlasmaCrete Armor
    0 ─────────────── 30 ─────────────── 100
                      │
                      └─ lightweight formulation research becomes available

The threshold does not randomly grant a bonus.

It deterministically makes a defined capability available.

The newly available program must still be researched, produced, tested, and fielded.

## 10. Research state

A research program should conceptually contain:

- stable program identity;
- current progress;
- maximum progress;
- allocation;
- maximum throughput;
- prerequisites;
- threshold unlocks;
- current generation;
- successor/branch programs;
- required facility;
- required personnel;
- required equipment;
- current status;
- completion history.

## 11. Why this matters

The same scheduling model can apply elsewhere.

Instead of:

    every frame:
      update everything

we can have:

    player input → immediate
    physical motion → continuous
    research → research checkpoint
    production → production checkpoint
    logistics → logistics checkpoint
    strategic simulation → strategic checkpoint
    maintenance → maintenance checkpoint

This gives raWWar a simulation architecture that resembles the world it is representing.

## 12. Game states versus phases

Some games appear to have phases.

A phase can mean two different things.

### Phase as state

For example:

    Planning → Staging → Deployment → Combat → Recovery

These are game states when they describe what the operation currently is.

### Phase as execution schedule

For example:

    Research evaluation → Production evaluation → Logistics evaluation

These are simulation checkpoints when they describe when a class of work is evaluated.

They should not be conflated.

## 13. Determinism

Given the same authoritative:

- world state;
- player decisions;
- research allocations;
- elapsed simulation time;
- procedures;
- inputs;

the resulting state should be reproducible.

Randomness may still exist where explicitly designed for bounded physical variation, personality, weather, combat uncertainty, or other game mechanics.

It should not secretly decide whether a deterministic research program succeeds.

## 14. Design principle

> **The world does not need to think every frame merely because the screen does.**

The Renderer is allowed to be fast.

The simulation is allowed to be deliberate.

The game state is authoritative.

The schedule is an implementation mechanism.

The player experiences the result as one living world.
