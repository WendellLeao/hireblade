using System;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeaponHolder
    {
        event Action<IWeapon> OnWeaponChanged;
        event Action OnWeaponExecuted;

        IWeapon Weapon { get; }

        void ChangeWeapon(WeaponData weaponData);
        void ExecuteWeapon();
        void FinishWeaponExecution();
    }
}
