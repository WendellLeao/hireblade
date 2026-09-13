using System;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Spells
{
    public interface ISpell : IPooledObject
    {
        event Action<ISpell> OnHit;
    }
}
