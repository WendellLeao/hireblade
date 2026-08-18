
using Hireblade.Gameplay.Spells;

namespace Hireblade.Gameplay.Weapons
{
    internal interface ISpellCaster
    {
        public void CastSpell();
        public void SetSpellFactory(ISpellFactory spellFactory);
    }
}
