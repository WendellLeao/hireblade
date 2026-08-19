# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Hireblade is a top-down action RPG (MOBA-style, point-and-click controls) built in Unity 6 (6000.5.9f1) with URP. See `README.md` for the gameplay pitch and a recruiter-facing architecture summary.

## Working with this repository

This is a Unity project with no external build/lint/test CLI scripts and no CI configured. All development happens through the Unity Editor (6000.5.9f1, matching `ProjectSettings/ProjectVersion.txt`).

- **Play the game:** open the project in Unity Editor, load `Assets/_Project/Scenes/Bootstrap.unity`, and press Play. Every other scene is loaded additively at runtime by `GameFlowManager` and is not meant to be pressed-play directly except for isolated debugging.
- **Run tests:** `Hireblade.Gameplay.Tests` (edit-mode NUnit) runs from the Editor's Test Runner window (`Window > General > Test Runner > EditMode`). It can also run headless via Unity's standard batchmode invocation, e.g. `Unity.exe -batchmode -projectPath . -runTests -testPlatform EditMode -testResults TestResults.xml -quit`.
- **Debug scenes:** `Assets/_Project/Scenes/Gameplay/EntitiesDebug.unity` and the `Hireblade.Debugging` assembly exist specifically to spawn/initialize individual entities (characters, enemies, weapons) in isolation, outside the normal `GameFlowManager` boot path.

## Architecture

### Assembly boundaries are the module boundaries

Source lives under `Assets/_Project/Source/<Hireblade.ModuleName>/`, one folder per `.asmdef`. The reference graph between these assemblies *is* the layering rule, not just a convention:

- `Hireblade.Core` : shared interfaces only (`IHealth`, `IGameFlowService`, `IInitializableAsync`). No gameplay logic, no MonoBehaviours beyond interfaces.
- `Hireblade.Application` : boot + game flow orchestration. References `Hireblade.Gameplay.UI` and `Hireblade.MainMenu`, nothing references it back.
- `Hireblade.Gameplay` : the core simulation (characters, enemies, weapons, spells, health, damage, navigation, cameras, particles). **Never references `Hireblade.Gameplay.UI`.**
- `Hireblade.Gameplay.Events` : typed `GameEvent` payloads (e.g. `HealthSpawnedEvent`) that `Hireblade.Gameplay` raises and `Hireblade.Gameplay.UI` subscribes to. This is the only channel between simulation and UI.
- `Hireblade.Gameplay.UI` : world-space/HUD UI. Only reacts to events; never called into by `Hireblade.Gameplay`.
- `Hireblade.MainMenu` : title screen flow.
- `Hireblade.Commands` : `ScriptableObject`-backed command/keybinding data (`CommandCollectionData`, `CommandType`).
- `Hireblade.Input`, `Hireblade.Utilities` : small, mostly dependency-free helpers.
- `Hireblade.Debugging` : editor/debug-only initializers, not referenced by runtime modules.

When adding a new script, put it in the assembly matching its concern, and if a new cross-assembly reference is needed, check whether it violates this direction (most commonly: gameplay code should never need to reference `Hireblade.Gameplay.UI`; route through an event in `Hireblade.Gameplay.Events` instead).

### Orchestrated lifecycle (no reliance on Script Execution Order)

Startup and scene transitions are driven explicitly rather than through Unity's Script Execution Order:

```
AppRoot.Start() [Bootstrap.unity, persistent]
  -> GameFlowManager.Initialize()
      -> EnterMainMenu() / EnterGameplay()
          -> additive scene load (async; editor-safe via EditorSceneManager, player via SceneManager)
          -> IGameFlowStateManager.EnterAsync(scene)
              -> MainMenuFlowManager -> MainMenuSystem.Initialize()
              -> GameplayFlowManager -> GameplaySystem.InitializeAsync() -> UISystem.Initialize()
                  -> CursorManager / CameraManager / ParticleManager / SpellManager
                     / WeaponManager / CharacterManager / EnemyManager
          -> previous scene is unloaded
```

Managers receive dependencies through an explicit `Initialize(...)` call from their parent system rather than resolving them internally. `GameplaySystem`/`UISystem` drive their own `Tick`/`LateTick` from `Update`/`LateUpdate`, so per-frame update order across subsystems is explicit at the call site, not implicit in script execution order settings.

### Service Locator + externally-published service packages

`WendellLeao.ServiceLocator` (`Locator.Register<T>()` / `Locator.Get<T>()`) exposes cross-cutting services as interfaces instead of static singletons: `IPoolingService`, `IEventService`, `IGameFlowService`, `IInputService`. These services are not project-local: they are standalone Unity packages (pulled in via `Packages/manifest.json` as git dependencies) maintained in their own repos:

- `service-locator`, `event-service`, `pooling-service`, `screen-service`, `audio-service`, `save-service`, `scene-switcher`, `unity-starter-kit` (all under `github.com/WendellLeao`)

If a change looks like it needs a fix inside one of these services rather than in Hireblade itself, it belongs in that package's own repo, not as a local patch here.

### Model/Controller/View split

`Health` is the reference example: `HealthData` (SO config) -> `HealthModel` (plain data/math) -> `HealthController` (MonoBehaviour implementing `IHealth`, exposes `OnHealthChanged`/`OnDepleted`) -> `HealthView` (UI, pooled, only listens to events/controller, never mutates simulation state). Follow this shape for new stat/resource systems rather than putting model math directly in a MonoBehaviour.

### Composition roots use interfaces, not concrete types

Entities such as `BasicEnemy` and `Character` cache sibling components exclusively through interfaces (`GetComponent<IDamageable>()`, `IWeaponHolder`, `IMoveableAgent`, `IHumanoidAnimatorController`, `ICommandInvoker`, `IDamageableView`, ...) and explicitly drive their `Initialize`/`Shutdown`/`Tick`. When adding a new capability to an entity, add an interface + component and wire it the same way rather than reaching for concrete types.

### Pooling and factories, not Instantiate/Destroy

Weapons, spells, particles, and health views are spawned through factories (`WeaponFactory`, `SpellFactory`, `ParticleFactory`) backed by `IPoolingService` and `PoolData` assets. New spawnable gameplay objects should follow the same factory + pool pattern rather than calling `Instantiate`/`Destroy` directly.

### Commands are data-driven and shared between player and AI

Actions implement `ICommand` (e.g. `AttackCommand`) and are resolved from `ScriptableObject`-backed `CommandCollectionData` by key. `CommandInputReader` drives commands from player input; `CommandAutoInvoker` drives the same `ICommand` interface for AI-controlled entities, so both paths execute through one shared abstraction (`ICommandInvoker`).

### Tests

`Hireblade.Gameplay.Tests` exercises real controllers (e.g. `HealthController` + `DamageController`) against a bare `HumbleEntity` GameObject with mocked `ScriptableObject` data (`SetXForTests` helpers guarded by `#if UNITY_EDITOR`), rather than mocking the components themselves. Follow this pattern for new gameplay tests: build a minimal real GameObject, mock only the data assets.

## Do not hand-edit

`.unity`, `.prefab`, `.asset`, and `.meta` files are Unity-serialized; edit them through the Unity Editor (or Unity MCP tooling), never by hand.
