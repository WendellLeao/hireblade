using UnityEngine;
using Hireblade.Gameplay.Spells;
using Random = UnityEngine.Random;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class Staff : MonoBehaviour, IWeapon, ISpellCaster
    {
        [SerializeField]
        private SpellData[] spellData;
        [SerializeField]
        private Transform spawnPoint;

        private ISpellFactory _spellFactory;
        private WeaponData _data;

        public WeaponData Data => _data;
        public string PoolId { get; set; }

        public void SetUp(WeaponData data)
        {
            _data = data;
        }

        public void Dispose()
        { }

        public void Execute()
        { }

        public void FinishExecution()
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
