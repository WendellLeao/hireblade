using UnityEngine;

namespace Hireblade.Gameplay.Spells
{
    public interface ISpellFactory
    {
        ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction);
    }
}
