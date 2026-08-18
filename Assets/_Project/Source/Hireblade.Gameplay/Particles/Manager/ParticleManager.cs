using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Particles.Manager
{
    public sealed class ParticleManager : MonoBehaviour
    {
        private ParticleFactory _particleFactory;

        public IParticleFactory Factory => _particleFactory;

        public void Initialize(IPoolingService poolingService)
        {
            _particleFactory = new ParticleFactory(poolingService);
        }

        private void OnDestroy()
        {
            _particleFactory?.Shutdown();
        }
    }
}
