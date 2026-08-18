using System;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Spells
{
    public interface ISpell : IPooledObject
    {
        public event Action<ISpell> OnHit;

        public void SetUp();
    }
}
