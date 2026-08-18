using System;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Spells
{
    internal sealed class ElementalBall : MonoBehaviour, ISpell, IParticleEmitter
    {
        public event Action<ISpell> OnHit;

        [Header("Data")]
        [SerializeField]
        private PoolData collisionParticlePoolData;

        private IParticleFactory _particleFactory;
        private ApplyForwardForce _applyForwardForce;
        private IDamager _damager;

        public string PoolId { get; set; }

        public void SetUp()
        {
            _applyForwardForce = GetComponent<ApplyForwardForce>();
            _damager = GetComponent<IDamager>();

            _applyForwardForce.SetUp();
        }

        private void OnTriggerEnter(Collider other)
        {
            _damager.TryApplyDamage(other);

            _particleFactory.EmitParticle(collisionParticlePoolData, transform.position, Quaternion.identity);

            OnHit?.Invoke(this);
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
