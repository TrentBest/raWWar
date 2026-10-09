# Coordinate Frames and Transform Contracts

> **A position is incomplete without its frame. A transform changes representation, not entity identity or authoritative world state.**

**Status:** Design contract; no universal raWWar coordinate convention is selected here.  
**Owner:** raWWar world-model implementation.  
**Audience:** Physics/orbit, navigation, observer, renderer, animation, and tooling contributors.  
**Evidence rule:** The repository now includes a generic Cartesian affine-transform primitive and executable contract checks. A universal frame graph, named canonical frames, world-wide axis/handedness conventions, and time-dependent transforms remain unimplemented.

## 1. Name the frame of every spatial quantity

A bare triple `(x, y, z)` is not enough to describe a physical position or direction. A spatial quantity must be interpreted in a named frame, with declared units and semantics. Examples of distinct frame roles include:

- a world or inertial frame;
- a parent body's local frame;
- an object's body-fixed frame;
- an observer or station frame;
- a presentation/camera frame.

These are examples of roles, not a declaration that every role already exists in the runtime. A frame's origin, axes, orientation convention, units, and relationship to its parent must be explicit in the relevant domain contract.

Do not use a camera-relative position as a canonical identity, persistence key, or authoritative physical state.

## 2. Conventions must be explicit, not inferred

Before a transform implementation becomes public contract, document the chosen conventions for:

- handedness and positive axis directions;
- angular units and positive rotation direction;
- transform representation (for example, matrix or quaternion) and normalization rules;
- whether a transform maps local coordinates to parent coordinates or the reverse;
- composition order;
- translation and distance units;
- invalid, singular, non-finite, or non-normalized inputs;
- numerical tolerance and determinism expectations.

The orbit evaluator currently returns a position described as inertial-frame coordinates, but that description alone does not settle every world-wide axis, handedness, body-frame, or renderer conversion convention. Do not infer a universal convention from one implementation.

## 3. Transform positions, directions, and orientations correctly

A position includes origin-relative location; a direction is a displacement/orientation without a translation component. APIs should not allow a direction to be accidentally transformed as a position. Orientation composition and scale/shear behavior must be explicit for the chosen representation.

If frame A is related to frame B by a transform, the API should make source and destination frames clear. Avoid ambiguous names such as `Transform(value)` when the source and destination cannot be determined from the type or call site.

A transform is a coordinate conversion. It does not move the underlying entity, advance simulation time, change ownership, or commit an event. Updating authoritative pose is a separate state transition.

## 4. Time and frame validity

Moving frames may depend on logical time. Any time-dependent transform must declare its valid time range, epoch, units, model version, and evaluation method. Observer and target poses used for one observation should be evaluated at a compatible logical time, or the time difference must be handled explicitly.

Do not let render-frame time silently become the authoritative time for orbital or world-state queries. Requests outside a transform's validity range must follow a declared policy instead of silently extrapolating.

## 5. Required executable properties

A transform implementation should have fixed examples and property tests covering:

- identity transform preserves valid positions, directions, and orientations;
- a transform followed by its inverse returns the original value within declared tolerance;
- composition order matches the documented convention;
- parent/local round trips remain consistent across nested frames;
- positions and directions treat translation differently;
- invalid and non-finite values are rejected or handled by an explicit policy;
- moving-frame transforms use the requested logical time and reject unsupported times;
- camera changes and frame conversion do not mutate canonical entity identity or authoritative state;
- fixed vectors detect accidental changes to handedness, axis mapping, units, or composition order.

Tolerance-based round trips alone are not sufficient: a pair of mutually wrong transforms can still invert each other. Include fixed reference vectors whose expected values follow from the explicitly chosen convention.

## 6. Implementation boundary and open decisions

Do not create a public transform API by selecting undocumented conventions for convenience. The creator/world-model owner must establish the canonical world frame and axis conventions when the game design and renderer integration require that choice. Until then, individual domains may document their local conventions and explicit adapters, but must not imply those are universal raWWar canon.

A first implementation slice should choose one narrow source/destination frame pair, define its units and convention, provide fixed reference vectors, and test composition and round trips before generalizing to a frame graph.
