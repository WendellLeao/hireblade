using UnityEngine;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeaponFactory
    {
        IWeapon CreateWeapon(WeaponData data, Transform parent);
        void ShutdownWeapon(IWeapon weapon);
    }
}
