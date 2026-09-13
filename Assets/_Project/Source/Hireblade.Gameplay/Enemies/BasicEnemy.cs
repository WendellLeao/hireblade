using System;
using Hireblade.Core.Health;
using NaughtyAttributes;
using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Animations;
using Hireblade.Gameplay.Commands;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Damage.View;
using Hireblade.Gameplay.Health;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Enemies
{
    public sealed class BasicEnemy : MonoBehaviour, IPooledObject
    {
        public event Action<BasicEnemy> OnDied;

        [Header("Components")]
        [SerializeField]
        private HealthController healthController;
        [SerializeField]
        private DamageController damageController;
        [SerializeField]
        private WeaponHolder weaponHolder;
        [SerializeField]
        private NavMeshTest navMeshTest;
        [SerializeField]
        private CommandAutoInvoker commandAutoInvoker;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private DamageableView damageableView;
        
        [Header("Data")]
        [SerializeField]
        private PoolData smokeParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private bool _isEnabled;

        public string PoolId { get; set; }
        public IHealth Health => healthController;

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;

            InitializeComponents();

            healthController.OnDepleted += OnDepleted;
        }

        public void Shutdown()
        {
            if (!_isEnabled)
            {
                return;
            }

            _isEnabled = false;

            weaponHolder.Shutdown();
            damageController.Shutdown();
            commandAutoInvoker.Shutdown();
            humanoidAnimatorController.Shutdown();
            damageableView.Shutdown();

            healthController.OnDepleted -= OnDepleted;
        }

        public void Tick(float deltaTime)
        {
            damageController.Tick(deltaTime);
            navMeshTest.Tick(deltaTime);
            commandAutoInvoker.Tick(deltaTime);
            humanoidAnimatorController.Tick(deltaTime);
            damageableView.Tick(deltaTime);
        }

        private void InitializeComponents()
        {
            healthController.Initialize();
            damageController.Initialize(healthController);
            weaponHolder.Initialize(_weaponFactory);
            navMeshTest.Initialize(cameraProvider: null, _particleFactory);
            commandAutoInvoker.Initialize(weaponHolder);
            humanoidAnimatorController.Initialize(healthController, damageController, weaponHolder, navMeshTest);
            damageableView.Initialize(_particleFactory, damageController);
        }

        private void OnDepleted()
        {
            GameObject smokeParticleObject = smokeParticlePoolData.Prefab;

            _particleFactory.EmitParticle(smokeParticlePoolData, transform.position, smokeParticleObject.transform.rotation);

            OnDied?.Invoke(this);
        }

        #region Debug & Testing

#if UNITY_EDITOR
        [Button("Initialize_Debug")]
        public void Initialize_Debug()
        {
            Initialize(_particleFactory, _weaponFactory);
        }

        [Button("Shutdown_Debug")]
        public void Shutdown_Debug()
        {
            Shutdown();
        }
#endif

        #endregion
    }
}
