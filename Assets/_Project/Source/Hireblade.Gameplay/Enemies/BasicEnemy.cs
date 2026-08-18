using System;
using Hireblade.Core;
using NaughtyAttributes;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Enemies
{
    public sealed class BasicEnemy : MonoBehaviour, IEnemy
    {
        public event Action<IEnemy> OnDied;

        [Header("Data")]
        [SerializeField]
        private PoolData smokeParticlePoolData;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private ICommandInvoker _commandInvoker;
        private IMoveableAgent _moveableAgent;
        private IHumanoidAnimatorController _humanoidAnimatorController;
        private IDamageableView _damageableView;
        private bool _isEnabled;

        public string PoolId { get; set; }
        public IHealth Health => _health;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            if (_isEnabled)
            {
                return;
            }

            _isEnabled = true;

            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;

            CacheComponents();

            SetUpComponents();

            _health.OnDepleted += HandleHealthDepleted;
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

            _health.OnDepleted -= HandleHealthDepleted;
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
            _moveableAgent.SetUp(cameraProvider: null, _particleFactory);
            _commandInvoker.SetUp(_weaponHolder);
            _humanoidAnimatorController.SetUp(_health, _damageable, _weaponHolder, _moveableAgent);
            _damageableView.SetUp(_particleFactory, _damageable);
        }

        private void HandleHealthDepleted()
        {
            GameObject smokeParticleObject = smokeParticlePoolData.Prefab;

            _particleFactory.EmitParticle(smokeParticlePoolData, transform.position, smokeParticleObject.transform.rotation);

            OnDied?.Invoke(this);
        }

#if UNITY_EDITOR
        [Button("SetUp_Debug")]
        public void SetUp_Debug()
        {
            SetUp(_particleFactory, _weaponFactory);
        }

        [Button("Dispose_Debug")]
        public void Dispose_Debug()
        {
            Dispose();
        }
#endif
    }
}
