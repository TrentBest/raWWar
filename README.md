# raWWar

## 01 🔷 Definition

**raWWar is a first-person war Experience, not an application.** It is a living military world that continues whether or not the player is looking at it.

The player inhabits a soldier inside a military system: a person with identity, qualifications, relationships, responsibilities, equipment, opportunities, history, and consequences.

> **What happens when I become one of the people who has to live here?**

## 02 🟢 Visual identity

![raWWar — The war is alive](docs/images/rawwar-hero.svg)

> **The soldiers themselves are the stars of this game.**

![One galaxy, one war](docs/images/rawwar-galactic-war.svg)

*One continuous war across a galaxy—not a stack of disposable maps.*

## 03 🔵 Plain-language explanation

![A living raWWar military installation](docs/images/rawwar-living-world.svg)

A launch crew can be working while another squad trains, researchers run experiments, logistics moves material, command reviews the situation, and soldiers spend their downtime together.

![A military base is a machine made of people](docs/images/rawwar-base-cutaway.svg)

**The world should always be doing something.** Wherever the player looks, there should be evidence of systems, people, procedures, decisions, work, failure, recovery, and consequence.

The universe is a **galaxy**. The Empire spans many star systems, and the campaign is a continuous war rather than a sequence of disposable maps. Warp drives exist, but a jump takes **days to initialize and prepare**; distance, logistics, reinforcement, and time therefore matter.

After choosing a faction, the player is introduced to the Empress. She orders the player to take a poorly defended but resource-rich enemy world and uphold the tithe. The player can obey or disobey. Either choice affects their relationship with Imperial authority and the larger galaxy.

![The Empress's first order](docs/images/rawwar-empress-order.svg)

> **This is not “pick a sector and play a map.” This is living inside the war.**

## 04 🟠 Audience / choose your path

- **New to raWWar:** begin with the [Game Design Document](docs/GAME_DESIGN_DOCUMENT.md).
- **Want the foundational design:** read the [Game Design Bible](docs/GAME_DESIGN_BIBLE.md).
- **Want to understand the technology boundary:** read [Experience Architecture](docs/EXPERIENCE_ARCHITECTURE.md).
- **Designing a system:** start at the [Design Documentation Index](docs/README.md), then follow the relevant deep dive.
- **Reviewing implementation status:** read [Technical Design](docs/TECHNICAL_DESIGN.md), [Production Plan](docs/PRODUCTION_PLAN.md), and [TODO.md](TODO.md).
- **Building or verifying the repository:** use the commands in section 08 and the evidence guidance in section 11.

## 05 🟣 At a glance

| Item | Current description |
|---|---|
| Type | Manifest-oriented game Experience |
| Primary host direction | AnyApp first; MyVR and other manifestations follow |
| Implementation baseline | .NET 8 |
| Architecture | Experience domain + manifest + MicroBundles + FSM_COS composition + host |
| Status | Active design and engineering; the end-to-end playable Experience is not yet complete |
| License | See [LICENSE.txt](LICENSE.txt) |

## 06 🟢 Responsibility boundary

raWWar owns the things that make **raWWar** raWWar:

- soldiers and identity;
- factions and military organizations;
- qualifications and careers;
- equipment and vehicles;
- combat and missions;
- research and technology;
- construction and infrastructure;
- terrain, weather, and world history;
- economics and social life;
- narrative and consequence.

The Singularity Workshop provides reusable machinery so raWWar does **not** have to reinvent the same infrastructure:

- **FSM_API** — state-machine behavior and state/context primitives;
- **MicroBundleDomain / MicroBundles** — capability contracts and independently defined capabilities;
- **FSM_COS** — composition and RuntimeAssembly handoff;
- **FSM_UserIO** — platform-neutral semantic user interaction;
- **AnyApp and other hosts** — execution and manifestation;
- **Workshop Renderer** — observer-relative presentation;
- persistence, networking, reconstruction, and related infrastructure as those capabilities mature.

![Event horizons](docs/images/rawwar-event-horizons.svg)

**FSM_COS is a central composition boundary, not the game, the host, or a renderer.** raWWar supplies meaning and domain rules; FSM_COS assembles requested capabilities; the host decides how the resulting runtime is executed or manifested. See [Experience Architecture](docs/EXPERIENCE_ARCHITECTURE.md).

## 07 🟠 Architecture and ecosystem

![raWWar Experience architecture](docs/images/rawwar-experience-architecture.svg)

```text
raWWar domain meaning and rules
              ↓
      Experience manifest
              ↓
       MicroBundle roots
              ↓
           FSM_COS
              ↓
       RuntimeAssembly
              ↓
       AnyApp / host / manifestation
```

The manifest identifies the requested composition; dependency closure determines what else must be present. The Experience should not quietly turn every transitive dependency into a root request, nor reimplement reusable Workshop machinery.

The current integration work is not yet proof of end-to-end host loading: the checked-in authoring/runtime manifests are distinct from AnyApp's publication manifest, and immutable artifact identity/dependency closure remain open integration requirements. The local FSM_COS composition check is scoped to its test adapter; it does not prove that AnyApp can retrieve and launch the full Experience. See the [AnyApp manifest bridge investigation](docs/integration/ANYAPP_MANIFEST_BRIDGE.md) and the [raWWar-to-AnyApp artifact-closure request](docs/requests/REQUEST-FROM-raWWar-AnyApp-ArtifactClosure.md).

## 08 🟩 Quick start

For readers, start with the [Game Design Document](docs/GAME_DESIGN_DOCUMENT.md). For a compact view of the intended boundary, read [Experience Architecture](docs/EXPERIENCE_ARCHITECTURE.md).

For developers with the .NET 8 SDK installed, from the repository root:

```bash
dotnet build raWWar.sln --configuration Release
dotnet run --project examples/KeplerOrbit/raWWar.KeplerOrbitExample.csproj
```

These commands build the solution and run the checked-in explicit-time Kepler-orbit example. They do **not** launch a playable game or prove end-to-end AnyApp composition.

## 09 🟪 Core concepts / how it works

raWWar is always experienced from the first-person perspective, but the role the player inhabits can grow dramatically:

- **Soldier** — qualify, serve, fight, repair, research, build, explore, survive.
- **Officer** — lead people, make decisions, manage pressure, command operations.
- **Cooperative** — share a war with players whose interests and objectives may differ.
- **Multiplayer** — fight free-for-all, team-versus-team, or persistent conflicts.
- **Persistent universes** — enter a universe whose history continues without you.

These are not separate games. They are different ways of inhabiting the same war.

![A soldier's career](docs/images/rawwar-soldier-progression.svg)

A soldier is not a class selected from a menu. Identity, qualification, responsibility, reputation, relationships, equipment, and history accumulate over time.

The repository distinguishes design certainty explicitly:

- **Canon** — established by the creator.
- **Candidate** — a direction under consideration.
- **Experiment** — something to prove through implementation.
- **Illumination Needed** — a question that still requires the creator's vision.

Implementation convenience must not silently become game design.

## 10 🟦 Usage and examples

Examples are engineering artifacts as well as documentation: they show how a capability is expected to be consumed and give contributors a concrete reference.

- [Examples index](examples/README.md) — checked-in usage examples and verification scope.
- [KeplerOrbit example](examples/KeplerOrbit/Program.cs) — explicit-time orbit queries, repeatability, periodicity, and invalid-boundary behavior.
- [Contract checks](tests/raWWar.ContractTests/Program.cs) — executable manifest, data, and architectural contract assertions.

Each example must say whether it is verified, source-shaped, or conceptual; name the version or commit it targets; cover meaningful boundaries; and state what it does not prove. An example is not automatically an integration test, playable feature, or release-readiness claim.

## 11 🩶 Verification and development

The repository is a design instrument, not a pile of feature promises. Its purpose is to make raWWar sufficiently clear that the next thing can actually be built.

The creator's descriptions are primary source material. The repository organizes those ideas, exposes contradictions, identifies missing rules, and turns the resulting design into a deterministic Experience shape.

The old engine-specific project shell has been removed. raWWar is not defined by a particular game engine or display technology. **AnyApp comes first; MyVR and other manifestations follow.**

For implementation claims, distinguish code and tests that exist today from design targets. The current `RaWWarMicroBundle.Load` remains a scaffold and `Arbitrate` returns `false`; the full station-and-rig first-person slice and end-to-end host loading remain future work. Consult [Technical Design](docs/TECHNICAL_DESIGN.md) and [TODO.md](TODO.md) for current evidence and blockers.

## 12 🟣 Documentation map / further reading

- [Game Design Document](docs/GAME_DESIGN_DOCUMENT.md) — living master description of the player experience.
- [Game Design Bible](docs/GAME_DESIGN_BIBLE.md) — foundational principles and world rules.
- [Experience Architecture](docs/EXPERIENCE_ARCHITECTURE.md) — the boundary between raWWar and reusable Workshop machinery.
- [Design Hub](docs/DESIGN_HUB.md) — intended public visual navigation model.
- [Vision and Pillars](docs/VISION_AND_PILLARS.md) — the core experience.
- [Gestures Design](docs/GESTURES_DESIGN.md) — physical soldier movement as data and FSM behavior.
- [Advisors and Command Staff](docs/ADVISORS_AND_STAFF.md) — qualified people, candidate records, specialist advice, and command appointments.
- [Renderer Integration](docs/RENDERER_INTEGRATION.md) — observing the world without making presentation its source of truth.
- [Visual asset catalogue](docs/images/README.md) — diagrams and artwork with their intended meaning.
- [Documentation conformance audit](docs/DOCUMENTATION_CONFORMANCE.md) — how this repository applies the shared Workshop documentation standard.

## 13 🟤 Related projects and Workshop identity

raWWar is one Experience in a modular ecosystem. It consumes reusable capabilities rather than making each Experience rebuild the same machinery.

- [FSM_API](https://github.com/TrentBest/FSM_API) — state-machine behavior.
- [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) — canonical MicroBundle domain contract.
- [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS) — composition kernel and RuntimeAssembly boundary.
- [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) — semantic user interaction.
- [AnyApp](https://github.com/TrentBest/AnyApp) — host and application proving ground.
- [WebPage](https://github.com/TrentBest/WebPage) — browser manifestation and Workshop proving ground.

The shared Workshop documentation convention is being defined in [FSM_COS's Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/docs/ecosystem-documentation-standard/DOCUMENTATION_STANDARD.md). raWWar applies the relevant section identifiers, color markers, boundary-first architecture, reader paths, evidence discipline, and example requirements while retaining its own game-design voice.

---

<p align="center">
  <em>The Singularity Workshop — Tools for the curious, the bold, and the systemically inclined.</em><br>
  <strong>Make the soldiers matter. Make the war alive.</strong>
</p>
