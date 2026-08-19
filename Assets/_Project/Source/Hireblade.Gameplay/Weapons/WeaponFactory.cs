using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Spells;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class WeaponFactory : IWeaponFactory
    {
        private readonly List<IWeapon> _weapons = new();

        private readonly IPoolingService _poolingService;
        private readonly IParticleFactory _particleFactory;
        private readonly ISpellFactory _spellFactory;

        public WeaponFactory(IPoolingService poolingService, IParticleFactory particleFactory, ISpellFactory spellFactory)
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

            weapon.Initialize(data);

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

        public void ShutdownWeapon(IWeapon weapon)
        {
            weapon.Shutdown();

            _weapons.Remove(weapon);

            _poolingService.ReleaseObjectToPool(weapon);
        }

        public void Shutdown()
        {
            for (int i = _weapons.Count - 1; i >= 0; i--)
            {
                ShutdownWeapon(_weapons[i]);
            }
        }
    }
}
