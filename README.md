# Hireblade

Hireblade is a top-down action RPG with MOBA-style, point-and-click controls set in a medieval fantasy world. Explore three increasingly difficult regions, recruit companions through dialogue, and switch control between party members on the fly as you build synergy between a tank, a ranged carry, and a support healer. Combat spoils earned from fights fuel stat upgrades for the whole party as you push toward the final region's boss fight.

<img width="1577" height="883" alt="Screenshot 2026-08-17 114653" src="https://github.com/user-attachments/assets/5a72ce89-a9a6-4966-b660-6180f99a9cbd" />

## Tech Stack

- **Engine:** Unity 6 (6000.3.10f1), Universal Render Pipeline
- **Async:** [UniTask](https://github.com/Cysharp/UniTask) for allocation-light async/await over coroutines
- **Navigation:** Unity NavMesh for point-and-click movement
- **Camera:** Cinemachine
- **Testing:** Unity Test Framework / NUnit (edit mode tests)
- **Editor tooling:** NaughtyAttributes for inspector ergonomics and in-editor debug buttons

## Architecture

Hireblade is split into focused assembly-definition modules under `Assets/_Project/Source`, each with a narrow responsibility and explicit references, so the compiler enforces the same boundaries the folder structure implies:

| Module | Responsibility |
|---|---|
| `Hireblade.Application` | Boots the app and orchestrates transitions between game flow states |
| `Hireblade.Core` | Cross-cutting interfaces shared across modules (`IHealth`, `IGameFlowService`) with no gameplay logic |
| `Hireblade.Gameplay` | Core simulation: characters, enemies, weapons, spells, health, damage, navigation, cameras, particles |
| `Hireblade.Gameplay.Events` | Typed event payloads that decouple gameplay from its subscribers |
| `Hireblade.Gameplay.UI` | World-space and screen UI (health bars, HUD) driven entirely by events, never by direct gameplay references |
| `Hireblade.MainMenu` | Title screen flow |
| `Hireblade.Commands` | Data-driven command definitions (`ScriptableObject`-backed key bindings) |
| `Hireblade.Input` | Input service abstraction |
| `Hireblade.Debugging` | Editor-only debug initializers and buttons, excluded from the dependency graph of runtime modules |
| `Hireblade.Utilities` | Small dependency-free helpers (scene queries, path/animator extensions) |

### Orchestrated lifecycle, not Script Execution Order

Rather than relying on Unity's Script Execution Order to guarantee init sequencing, Hireblade drives startup and scene transitions through an explicit root orchestrator:

```
AppRoot.Start()
  → GameFlowManager.Initialize()
      → EnterMainMenu() / EnterGameplay()
          → additive scene load (async, editor & player safe)
          → IGameFlowStateManager.EnterAsync(scene)
              → MainMenuFlowManager  → MainMenuSystem.Initialize()
              → GameplayFlowManager  → GameplaySystem.InitializeAsync() → UISystem.Initialize()
                  → CursorManager / CameraManager / ParticleManager / SpellManager
                    / WeaponManager / CharacterManager / EnemyManager
```

Each manager receives its dependencies through an explicit `Initialize(...)` call rather than resolving them itself, so construction order and data flow are visible at the call site instead of hidden behind lifecycle timing. `GameplaySystem` and `UISystem` then drive their own `Tick`/`LateTick` from `Update`/`LateUpdate`, giving full control over per-frame update order across dozens of subsystems.

### Service Locator + self-authored service packages

Cross-cutting services (pooling, events, input, screens, game flow) are exposed through a lightweight [Service Locator](https://github.com/WendellLeao/service-locator) rather than static singletons, keeping consumers testable against interfaces (`IPoolingService`, `IEventService`, `IGameFlowService`, ...). These services aren't bundled with the game: they're a small suite of standalone Unity packages I built and published independently and pull into this project as git dependencies via the package manifest:

- [`service-locator`](https://github.com/WendellLeao/service-locator)
- [`event-service`](https://github.com/WendellLeao/event-service)
- [`pooling-service`](https://github.com/WendellLeao/pooling-service)
- [`screen-service`](https://github.com/WendellLeao/screen-service)
- [`audio-service`](https://github.com/WendellLeao/audio-service)
- [`save-service`](https://github.com/WendellLeao/save-service)
- [`scene-search-window`](https://github.com/WendellLeao/scene-search-window)
- [`unity-starter-kit`](https://github.com/WendellLeao/unity-starter-kit)

### Event-driven decoupling between gameplay and UI

`Hireblade.Gameplay` never references `Hireblade.Gameplay.UI` (the asmdef graph forbids it). Instead, gameplay raises typed events, e.g. `HealthSpawnedEvent`, through `IEventService`, and `HealthViewManager` in the UI module subscribes independently, pools a `HealthView`, and drives its own tick. Gameplay code has no idea a health bar exists.

### Model / Controller / View, applied consistently

Health is a representative example of the split used throughout: `HealthData` (ScriptableObject config) feeds `HealthModel` (plain data + math), which `HealthController` (MonoBehaviour, `IHealth`) wraps and exposes through `OnHealthChanged`/`OnDepleted` events, fully decoupled from `HealthView`, which only listens and never touches simulation state.

### Dumb/smart component composition

Entities like `BasicEnemy` and `Character` act as composition roots: they cache sibling components strictly through interfaces (`GetComponent<IDamageable>()`, `IWeaponHolder`, `IMoveableAgent`, `IHumanoidAnimatorController`, ...), wire their `Initialize`/`Shutdown`/`Tick` calls explicitly, and stay agnostic to the concrete implementation behind each interface. Any component can be swapped without touching the entity.

### Object pooling and factories

Weapons, spells, particles, and health views are all spawned through factories (`WeaponFactory`, `SpellFactory`, `ParticleFactory`) backed by the pooling service, avoiding runtime `Instantiate`/`Destroy` churn during combat.

### Data-driven commands

Player and AI actions are modeled as `ICommand` objects (`AttackCommand`, ...) resolved from `ScriptableObject`-backed `CommandCollectionData`. `CommandInputReader` maps key bindings to commands for the player; `CommandAutoInvoker` drives the same command interface for AI-controlled entities, so both share one execution path.

### Tests

`Hireblade.Gameplay.Tests` runs edit-mode NUnit tests against the real `HealthController`/`DamageController` pair using mocked `ScriptableObject` data, exercising the actual damage math rather than a mocked model.

## Project Structure

```
Assets/_Project/
├── Scenes/
│   ├── Bootstrap.unity        # persistent scene: AppRoot + service bootstrapping
│   ├── UI/MainMenu.unity      # loaded additively by GameFlowManager
│   └── Gameplay/Gameplay.unity
└── Source/
    ├── Hireblade.Application
    ├── Hireblade.Core
    ├── Hireblade.Gameplay
    ├── Hireblade.Gameplay.Events
    ├── Hireblade.Gameplay.UI
    ├── Hireblade.Gameplay.Tests
    ├── Hireblade.MainMenu
    ├── Hireblade.Commands
    ├── Hireblade.Input
    ├── Hireblade.Debugging
    └── Hireblade.Utilities
```

## Gallery

<img width="1577" height="882" alt="Screenshot 2026-08-17 113918" src="https://github.com/user-attachments/assets/8e409ad8-9611-40ee-8c39-1c50d6a11e8a" />
<img width="1580" height="882" alt="Screenshot 2026-08-17 114207" src="https://github.com/user-attachments/assets/aea36e41-ce16-436f-be53-4e1c7e28f130" />
