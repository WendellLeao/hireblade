using WendellLeao.Pooling;
using UnityEngine;

namespace Hireblade.Gameplay.Particles
{
    public interface IParticleFactory
    {
        IParticle EmitParticle(PoolData particlePoolData, Transform parent);
        IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation);
        void StopParticle(IParticle particle);
    }
}
