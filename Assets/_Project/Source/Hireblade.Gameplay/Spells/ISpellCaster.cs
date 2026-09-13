namespace Hireblade.Gameplay.Spells
{
    internal interface ISpellCaster
    {
        void CastSpell();
        void SetSpellFactory(ISpellFactory spellFactory);
    }
}
