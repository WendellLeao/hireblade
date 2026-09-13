using System;
using Hireblade.Core.Health;
using UnityEngine;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Weapons;
using Random = UnityEngine.Random;

namespace Hireblade.Gameplay.Animations
{
    internal sealed class HumanoidAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float velocityDampTime = 0.08f;

        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int MovesetType = Animator.StringToHash("MovesetType");
        private static readonly int ExecuteWeapon = Animator.StringToHash("ExecuteWeapon");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        private static readonly int DeathType = Animator.StringToHash("DeathType");
        private static readonly int Die = Animator.StringToHash("Die");

        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IMoveableAgent _moveableAgent;
        private float _smoothedSpeed;

        public void Initialize(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder, IMoveableAgent moveableAgent)
        {
            _health = health;
            _damageable = damageable;
            _weaponHolder = weaponHolder;
            _moveableAgent = moveableAgent;

            OnWeaponChanged(_weaponHolder.Weapon);

            SubscribeEvents();
        }

        public void Shutdown()
        {
            UnsubscribeEvents();
        }

        public void Tick(float deltaTime)
        {
            SetVelocity(_moveableAgent.Velocity.magnitude, deltaTime);
        }

        private void SubscribeEvents()
        {
            _health.OnDepleted += OnHealthDepleted;

            _damageable.OnDamageTaken += OnDamageTaken;

            _weaponHolder.OnWeaponChanged += OnWeaponChanged;
            _weaponHolder.OnWeaponExecuted += OnWeaponExecuted;
        }

        private void UnsubscribeEvents()
        {
            _health.OnDepleted -= OnHealthDepleted;

            _damageable.OnDamageTaken -= OnDamageTaken;

            _weaponHolder.OnWeaponChanged -= OnWeaponChanged;
            _weaponHolder.OnWeaponExecuted -= OnWeaponExecuted;
        }

        private void OnHealthDepleted()
        {
            int randomDeathType = Random.Range(0, Enum.GetValues(typeof(DeathType)).Length);

            animator.SetInteger(id: DeathType, randomDeathType);
            animator.SetTrigger(id: Die);
        }

        private void OnDamageTaken(DamageData damageData)
        {
            animator.SetTrigger(id: TakeDamage);
        }

        private void OnWeaponChanged(IWeapon weapon)
        {
            WeaponData weaponData = weapon.Data;

            animator.SetInteger(id: MovesetType, (int)weaponData.MovesetType);
        }

        private void OnWeaponExecuted()
        {
            animator.SetTrigger(id: ExecuteWeapon);
        }

        private void SetVelocity(float velocityMagnitude, float deltaTime)
        {
            animator.SetFloat(id: Velocity, velocityMagnitude, velocityDampTime, deltaTime);
        }
    }
}
