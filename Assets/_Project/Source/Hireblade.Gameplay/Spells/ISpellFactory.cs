using UnityEngine;

namespace Hireblade.Gameplay.Spells
{
    public interface ISpellFactory
    {
        public ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction);
    }
}
