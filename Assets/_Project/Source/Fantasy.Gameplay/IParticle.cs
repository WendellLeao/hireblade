using System;
using WendellLeao.Pooling;

namespace Fantasy.Gameplay
{
    public interface IParticle : IPooledObject
    {
        public event Action<IParticle> OnCompleted;

        public void SetUp();
        public void Dispose();
    }
}
