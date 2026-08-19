using Cysharp.Threading.Tasks;
using Hireblade.Gameplay.Cameras.Manager;
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
    public sealed class GameplaySystem : MonoBehaviour
    {
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

        private bool _isInitialized;

        public UniTask InitializeAsync()
        {
            IPoolingService poolingService = Locator.Get<IPoolingService>();
            IEventService eventService = Locator.Get<IEventService>();

            particleManager.Initialize(poolingService);
            spellManager.Initialize(poolingService, particleManager.Factory);
            weaponManager.Initialize(poolingService, particleManager.Factory, spellManager.Factory);
            characterManager.Initialize(poolingService, eventService, particleManager.Factory, weaponManager.Factory, cameraManager);
            enemyManager.Initialize(poolingService, eventService, particleManager.Factory, weaponManager.Factory);

            _isInitialized = true;

            return UniTask.CompletedTask;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            float deltaTime = Time.deltaTime;

            characterManager.Tick(deltaTime);
            enemyManager.Tick(deltaTime);
        }
    }
}
