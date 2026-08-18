using System;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Particles
{
    public interface IParticle : IPooledObject
    {
        public event Action<IParticle> OnCompleted;

        public void SetUp();
        public void Dispose();
    }
}
