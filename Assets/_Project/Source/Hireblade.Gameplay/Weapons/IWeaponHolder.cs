using System;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;

        public IWeapon Weapon { get; }

        public void Initialize(IWeaponFactory weaponFactory);
        public void Shutdown();
        public void ChangeWeapon(WeaponData weaponData);
        public void ExecuteWeapon();
        public void FinishWeaponExecution();
    }
}
