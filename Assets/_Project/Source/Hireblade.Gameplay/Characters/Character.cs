using System;
using Hireblade.Core.Health;
using NaughtyAttributes;
using UnityEngine;
using Hireblade.Gameplay.Animations;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Commands;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Damage.View;
using Hireblade.Gameplay.Health;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Characters
{
    public sealed class Character : MonoBehaviour, IPooledObject
    {
        public event Action<Character> OnDied;
        
        [SerializeField]
        private HealthController healthController;
        [SerializeField]
        private DamageController damageController;
        [SerializeField]
        private WeaponHolder weaponHolder;
        [SerializeField]
        private NavMeshClickMover navMeshClickMover;
        [SerializeField]
        private CommandInputReader commandInputReader;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private DamageableView damageableView;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;
        private bool _isEnabled;

        public string PoolId { get; set; }
        public IHealth Health => healthController;

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;

            InitializeComponents();

            _cameraProvider.SetVirtualCameraTarget(transform);

            SubscribeEvent();
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
            commandInputReader.Shutdown();
            humanoidAnimatorController.Shutdown();
            damageableView.Shutdown();

            UnsubscribeEvent();
        }

        public void Tick(float deltaTime)
        {
            damageController.Tick(deltaTime);
            navMeshClickMover.Tick(deltaTime);
            commandInputReader.Tick(deltaTime);
            humanoidAnimatorController.Tick(deltaTime);
            damageableView.Tick(deltaTime);
        }

        private void InitializeComponents()
        {
            healthController.Initialize();
            damageController.Initialize(healthController);
            weaponHolder.Initialize(_weaponFactory);
            navMeshClickMover.Initialize(_cameraProvider, _particleFactory);
            commandInputReader.Initialize(weaponHolder);
            humanoidAnimatorController.Initialize(healthController, damageController, weaponHolder, navMeshClickMover);
            damageableView.Initialize(_particleFactory, damageController);
        }

        private void SubscribeEvent()
        {
            healthController.OnDepleted += HandleHealthDepleted;

            weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }

        private void UnsubscribeEvent()
        {
            healthController.OnDepleted -= HandleHealthDepleted;

            weaponHolder.OnWeaponExecuted -= HandleWeaponExecute;
        }

        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }

        private void HandleWeaponExecute()
        {
            navMeshClickMover.ResetPath();
        }

        #region Debug & Testing

#if UNITY_EDITOR
        [Button("Initialize_Debug")]
        public void Initialize_Debug()
        {
            Initialize(_particleFactory, _weaponFactory, _cameraProvider);
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
