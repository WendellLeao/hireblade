using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Damage;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class Sword : BaseWeapon, IMeleeWeapon
    {
        [Header("Components")]
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private Damager damager;

        [Header("Data")]
        [SerializeField]
        private PoolData bloodParticlesPoolData;

        private bool _isEnabled;

        public override void Execute()
        {
            SetColliderEnabled(false);
        }

        public override void FinishExecution()
        {
            SetColliderEnabled(false);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetColliderEnabled(false);

            _isEnabled = true;
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();

            _isEnabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled)
            {
                return;
            }

            damager.TryApplyDamage(other);

            EmitParticle();
        }

        private void EmitParticle()
        {
            ParticleFactory.EmitParticle(bloodParticlesPoolData, transform.position, Quaternion.identity);
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            capsuleCollider.enabled = isEnabled;
        }
    }
}
