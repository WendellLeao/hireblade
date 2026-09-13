using System;
using Hireblade.Gameplay.Particles;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Spells
{
    internal abstract class BaseSpell : MonoBehaviour, ISpell
    {
        public event Action<ISpell> OnHit;
        
        [Header("Data")]
        [SerializeField]
        private PoolData collisionParticlePoolData;
        
        private IParticleFactory _particleFactory;
        
        public string PoolId { get; set; }
        
        public void Initialize(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;

            OnInitialize();
        }

        public void Shutdown()
        {
            OnShutdown();
        }

        protected virtual void OnInitialize()
        { }

        protected virtual void OnShutdown()
        { }

        protected virtual void OnTriggerEnter(Collider other)
        {
            EmitParticle();

            OnHit?.Invoke(this);
        }

        private void EmitParticle()
        {
            _particleFactory.EmitParticle(collisionParticlePoolData, transform.position, Quaternion.identity);
        }
    }
}
