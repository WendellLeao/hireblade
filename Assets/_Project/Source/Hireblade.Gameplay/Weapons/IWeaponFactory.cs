using UnityEngine;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeaponFactory
    {
        public IWeapon CreateWeapon(WeaponData data, Transform parent);
        public void ShutdownWeapon(IWeapon weapon);
    }
}
