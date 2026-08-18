using System;
using WendellLeao.Pooling;

namespace Fantasy.Gameplay
{
    public interface ISpell : IPooledObject
    {
        public event Action<ISpell> OnHit;

        public void SetUp();
    }
}
