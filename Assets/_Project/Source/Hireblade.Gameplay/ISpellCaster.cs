namespace Hireblade.Gameplay
{
    internal interface ISpellCaster
    {
        public void CastSpell();
        public void SetSpellFactory(ISpellFactory spellFactory);
    }
}
