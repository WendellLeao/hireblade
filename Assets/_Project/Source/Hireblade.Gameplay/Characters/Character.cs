using System;
using Hireblade.Core;
using NaughtyAttributes;
using UnityEngine;
using Hireblade.Gameplay.Animations;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Commands;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Characters
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        public event Action<ICharacter> OnDied;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;
        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IMoveableAgent _moveableAgent;
        private ICommandInvoker _commandInvoker;
        private IHumanoidAnimatorController _humanoidAnimatorController;
        private IDamageableView _damageableView;
        private bool _isEnabled;

        public string PoolId { get; set; }
        public IHealth Health => _health;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;

            CacheComponents();

            SetUpComponents();

            _cameraProvider.VirtualCamera.SetTarget(transform);

            SubscribeEvent();
        }

        public void Dispose()
        {
            if (!_isEnabled)
            {
                return;
            }

            _isEnabled = false;

            _weaponHolder.Dispose();
            _damageable.Dispose();
            _commandInvoker.Dispose();
            _humanoidAnimatorController.Dispose();
            _damageableView.Dispose();

            UnsubscribeEvent();
        }

        public void Tick(float deltaTime)
        {
            _damageable.Tick(deltaTime);
            _moveableAgent.Tick(deltaTime);
            _commandInvoker.Tick(deltaTime);
            _humanoidAnimatorController.Tick(deltaTime);
            _damageableView.Tick(deltaTime);
        }

        private void CacheComponents()
        {
            _health = GetComponent<IHealth>();
            _damageable = GetComponent<IDamageable>();
            _weaponHolder = GetComponent<IWeaponHolder>();
            _moveableAgent = GetComponent<IMoveableAgent>();
            _commandInvoker = GetComponent<ICommandInvoker>();
            _humanoidAnimatorController = GetComponent<IHumanoidAnimatorController>();
            _damageableView = GetComponent<IDamageableView>();
        }

        private void SetUpComponents()
        {
            _health.SetUp();
            _damageable.SetUp(_health);
            _weaponHolder.SetUp(_weaponFactory);
            _moveableAgent.SetUp(_cameraProvider, _particleFactory);
            _commandInvoker.SetUp(_weaponHolder);
            _humanoidAnimatorController.SetUp(_health, _damageable, _weaponHolder, _moveableAgent);
            _damageableView.SetUp(_particleFactory, _damageable);
        }

        private void SubscribeEvent()
        {
            _health.OnDepleted += HandleHealthDepleted;

            _weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }

        private void UnsubscribeEvent()
        {
            _health.OnDepleted -= HandleHealthDepleted;

            _weaponHolder.OnWeaponExecuted -= HandleWeaponExecute;
        }

        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }

        private void HandleWeaponExecute()
        {
            _moveableAgent.ResetPath();
        }

#if UNITY_EDITOR
        [Button("SetUp_Debug")]
        public void SetUp_Debug()
        {
            SetUp(_particleFactory, _weaponFactory, _cameraProvider);
        }

        [Button("Dispose_Debug")]
        public void Dispose_Debug()
        {
            Dispose();
        }
#endif
    }
}
