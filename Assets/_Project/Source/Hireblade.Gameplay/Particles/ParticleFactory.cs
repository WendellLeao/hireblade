using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Particles
{
    internal sealed class ParticleFactory : IParticleFactory
    {
        private readonly List<SimpleParticle> _particles = new();

        private readonly IPoolingService _poolingService;

        public ParticleFactory(IPoolingService poolingService)
        {
            _poolingService = poolingService;
        }

        public void Shutdown()
        {
            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                StopParticle(_particles[i]);
            }
        }
        
        public IParticle EmitParticle(PoolData particlePoolData, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(particlePoolData.Id, parent, out SimpleParticle particle))
            {
                return null;
            }

            _particles.Add(particle);

            particle.Initialize();

            particle.OnCompleted += StopParticle;

            return particle;
        }

        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation)
        {
            IParticle particle = EmitParticle(particlePoolData, parent: null);

            particle.transform.SetPositionAndRotation(position, rotation);

            return particle;
        }

        public void StopParticle(IParticle particle)
        {
            SimpleParticle simpleParticle = (SimpleParticle)particle;
            
            simpleParticle.Shutdown();
            
            simpleParticle.OnCompleted -= StopParticle;

            _particles.Remove(simpleParticle);

            _poolingService.ReleaseObjectToPool(particle);
        }
    }
}
