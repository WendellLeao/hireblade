using Fantasy.Gameplay.Cameras.Manager;
using Fantasy.Gameplay.Cursor.Manager;
using Fantasy.Gameplay.Enemies.Manager;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Spells.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using Fantasy.Gameplay.Characters.Manager;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using WendellLeao.ServiceLocator;

namespace Fantasy.Gameplay.System
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

            cursorManager.SetUp();
            particleManager.SetUp(poolingService);
            spellManager.SetUp(poolingService, particleManager);
            weaponManager.SetUp(poolingService, particleManager, spellManager);
            characterManager.SetUp(poolingService, eventService, particleManager, weaponManager, cameraManager);
            enemyManager.SetUp(poolingService, eventService, particleManager, weaponManager);
        }
    }
}
