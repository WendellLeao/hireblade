using WendellLeao.Pooling;
using UnityEngine;

namespace Hireblade.Gameplay
{
    public interface IParticleFactory
    {
        public IParticle EmitParticle(PoolData particlePoolData, Transform parent);
        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation);
        public void DisposeParticle(IParticle particle);
    }
}
