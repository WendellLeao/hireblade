using Hireblade.Gameplay.Cameras.Manager;
using Hireblade.Gameplay.Cursor.Manager;
using Hireblade.Gameplay.Enemies.Manager;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Spells.Manager;
using Hireblade.Gameplay.Weapons.Manager;
using Hireblade.Gameplay.Characters.Manager;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using WendellLeao.ServiceLocator;

namespace Hireblade.Gameplay.System
{
    internal sealed class GameplaySystem : MonoBehaviour
    {
        [SerializeField]
        private CursorManager cursorManager;
        [SerializeField]
        private CameraManager cameraManager;
        [SerializeField]
        private ParticleManager particleManager;
        [SerializeField]
        private SpellManager spellManager;
        [SerializeField]
        private WeaponManager weaponManager;
        [SerializeField]
        private CharacterManager characterManager;
        [SerializeField]
        private EnemyManager enemyManager;

        private void Awake()
        {
            IPoolingService poolingService = Locator.Get<IPoolingService>();
            IEventService eventService = Locator.Get<IEventService>();

            cursorManager.Initialize();
            particleManager.Initialize(poolingService);
            spellManager.Initialize(poolingService, particleManager.Factory);
            weaponManager.Initialize(poolingService, particleManager.Factory, spellManager.Factory);
            characterManager.Initialize(poolingService, eventService, particleManager.Factory, weaponManager.Factory, cameraManager);
            enemyManager.Initialize(poolingService, eventService, particleManager.Factory, weaponManager.Factory);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            characterManager.Tick(deltaTime);
            enemyManager.Tick(deltaTime);
        }
    }
}
