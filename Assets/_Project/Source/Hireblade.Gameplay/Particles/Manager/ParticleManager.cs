using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Particles.Manager
{
    public sealed class ParticleManager : MonoBehaviour, IParticleFactory
    {
        private readonly List<IParticle> _particles = new();

        private IPoolingService _poolingService;

        public void SetUp(IPoolingService poolingService)
        {
            _poolingService = poolingService;
        }

        public IParticle EmitParticle(PoolData particlePoolData, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(particlePoolData.Id, parent, out IParticle particle))
            {
                return null;
            }

            _particles.Add(particle);

            particle.SetUp();

            particle.OnCompleted += DisposeParticle;

            return particle;
        }

        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation)
        {
            IParticle particle = EmitParticle(particlePoolData, parent: null);

            particle.transform.SetPositionAndRotation(position, rotation);

            return particle;
        }

        public void DisposeParticle(IParticle particle)
        {
            particle.Dispose();

            particle.OnCompleted -= DisposeParticle;

            _particles.Remove(particle);

            _poolingService.ReleaseObjectToPool(particle);
        }

        private void OnDestroy()
        {
            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                DisposeParticle(_particles[i]);
            }
        }
    }
}
