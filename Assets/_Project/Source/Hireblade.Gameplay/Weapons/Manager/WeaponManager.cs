using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Spells;

namespace Hireblade.Gameplay.Weapons.Manager
{
    public sealed class WeaponManager : MonoBehaviour, IWeaponFactory
    {
        private readonly List<IWeapon> _weapons = new();

        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;
        private ISpellFactory _spellFactory;

        public void SetUp(IPoolingService poolingService, IParticleFactory particleFactory, ISpellFactory spellFactory)
        {
            _poolingService = poolingService;
            _particleFactory = particleFactory;
            _spellFactory = spellFactory;
        }

        public IWeapon CreateWeapon(WeaponData data, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(data.PoolData.Id, parent, out IWeapon weapon))
            {
                return null;
            }

            _weapons.Add(weapon);

            weapon.SetUp(data);

            if (weapon is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }

            if (weapon is ISpellCaster spellCaster)
            {
                spellCaster.SetSpellFactory(_spellFactory);
            }

            return weapon;
        }

        public void DisposeWeapon(IWeapon weapon)
        {
            weapon.Dispose();

            _weapons.Remove(weapon);

            _poolingService.ReleaseObjectToPool(weapon);
        }

        private void OnDestroy()
        {
            for (int i = _weapons.Count - 1; i >= 0; i--)
            {
                DisposeWeapon(_weapons[i]);
            }
        }
    }
}
