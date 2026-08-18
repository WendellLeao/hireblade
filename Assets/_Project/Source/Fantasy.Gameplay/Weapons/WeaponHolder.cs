using System;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class WeaponHolder : MonoBehaviour, IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;

        [SerializeField]
        private WeaponData data;
        [SerializeField]
        private Transform parent;

        private IWeaponFactory _weaponFactory;
        private IWeapon _weapon;
        private bool _isEnabled;

        public IWeapon Weapon => _weapon;

        public void SetUp(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;

            _isEnabled = true;

            ChangeWeapon(data);
        }

        public void Dispose()
        {
            _isEnabled = false;

            _weapon?.Dispose();
        }

        public void ChangeWeapon(WeaponData weaponData)
        {
            if (!_isEnabled)
            {
                return;
            }

            DisposeWeapon();

            _weapon = _weaponFactory.CreateWeapon(weaponData, parent);

            OnWeaponChanged?.Invoke(_weapon);
        }

        [Button]
        public void ExecuteWeapon()
        {
            if (!_isEnabled)
            {
                return;
            }

            _weapon.Execute();

            OnWeaponExecuted?.Invoke();
        }

        public void FinishWeaponExecution()
        {
            _weapon.FinishExecution();
        }

        private void DisposeWeapon()
        {
            if (_weapon == null)
            {
                return;
            }

            _weaponFactory.DisposeWeapon(_weapon);
        }
    }
}
