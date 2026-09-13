namespace Hireblade.Gameplay.Spells
{
    internal interface ISpellCaster
    {
        public void CastSpell();
        public void SetSpellFactory(ISpellFactory spellFactory);
    }
}
