using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class Sword : BaseWeapon, IMeleeWeapon, IParticleEmitter
    {
        [Header("Components")]
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private Damager damager;
        
        [Header("Data")]
        [SerializeField]
        private PoolData bloodParticlesPoolData;

        private IParticleFactory _particleFactory;
        private bool _isEnabled;

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

        public override void Execute()
        {
            SetColliderEnabled(false);
        }

        public override void FinishExecution()
        {
            SetColliderEnabled(false);
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
            _particleFactory.EmitParticle(bloodParticlesPoolData, transform.position, Quaternion.identity);
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            capsuleCollider.enabled = isEnabled;
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
