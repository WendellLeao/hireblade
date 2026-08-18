using Hireblade.Gameplay.Cameras.Manager;
using Hireblade.Gameplay.Cursor.Manager;
using Hireblade.Gameplay.Enemies.Manager;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Spells;
using Hireblade.Gameplay.Spells.Manager;
using Hireblade.Gameplay.Weapons;
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

            cursorManager.SetUp();

            particleManager.SetUp(poolingService);

            IParticleFactory particleFactory = particleManager.Factory;

            spellManager.SetUp(poolingService, particleFactory);

            ISpellFactory spellFactory = spellManager.Factory;

            weaponManager.SetUp(poolingService, particleFactory, spellFactory);

            IWeaponFactory weaponFactory = weaponManager.Factory;

            characterManager.SetUp(poolingService, eventService, particleFactory, weaponFactory, cameraManager);
            enemyManager.SetUp(poolingService, eventService, particleFactory, weaponFactory);
        }
    }
}
