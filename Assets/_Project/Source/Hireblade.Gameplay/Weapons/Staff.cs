using UnityEngine;
using Hireblade.Gameplay.Spells;
using Random = UnityEngine.Random;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class Staff : BaseWeapon, ISpellCaster
    {
        [SerializeField]
        private SpellData[] spellData;
        [SerializeField]
        private Transform spawnPoint;

        private ISpellFactory _spellFactory;

        public override void Execute()
        { }

        public override void FinishExecution()
        { }

        public void CastSpell()
        {
            SpellData randomSpellData = spellData[Random.Range(0, spellData.Length)];

            _spellFactory.CastSpell(randomSpellData, spawnPoint.position, direction: transform.forward);
        }

        public void SetSpellFactory(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }
    }
}
