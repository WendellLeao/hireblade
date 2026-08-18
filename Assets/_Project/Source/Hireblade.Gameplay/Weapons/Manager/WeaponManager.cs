using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Spells;

namespace Hireblade.Gameplay.Weapons.Manager
{
    public sealed class WeaponManager : MonoBehaviour
    {
        private WeaponFactory _weaponFactory;

        public IWeaponFactory Factory => _weaponFactory;

        public void Initialize(IPoolingService poolingService, IParticleFactory particleFactory, ISpellFactory spellFactory)
        {
            _weaponFactory = new WeaponFactory(poolingService, particleFactory, spellFactory);
        }

        private void OnDestroy()
        {
            _weaponFactory?.Shutdown();
        }
    }
}
