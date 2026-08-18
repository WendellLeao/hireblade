using UnityEngine;

namespace Hireblade.Gameplay
{
    public interface IWeaponFactory
    {
        public IWeapon CreateWeapon(WeaponData data, Transform parent);
        public void DisposeWeapon(IWeapon weapon);
    }
}
