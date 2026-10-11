# Visualizer documentation moved to its implementation repository

The dedicated repository now owns the authoritative Visualizer architecture, implementation, and GPU strategy:

- [TheSingularityWorkshop.Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize)
- [Architecture](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/docs/ARCHITECTURE.md)
- [Gesture and pose pipeline](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/docs/GESTURES_AND_POSE_PIPELINE.md)
- [GPU backend strategy](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/docs/GPU_BACKEND_STRATEGY.md)
- [Implementation roadmap](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/docs/IMPLEMENTATION_ROADMAP.md)

This folder remains as a historical starting point from the initial raWWar architecture exploration. New Visualizer design and implementation decisions should be made in the dedicated repository so they have one authoritative home.

The current state is still an early contract scaffold: no real GPU backend or WPF presentation surface has been proven yet. The project is not ready for a NuGet release.
