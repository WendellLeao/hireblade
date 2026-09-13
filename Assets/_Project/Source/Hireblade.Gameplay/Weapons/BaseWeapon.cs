using UnityEngine;

namespace Hireblade.Gameplay.Weapons
{
    internal abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        private WeaponData _data;

        public string PoolId { get; set; }
        public WeaponData Data => _data;
        
        public abstract void Execute();
        
        public abstract void FinishExecution();

        public void Initialize(WeaponData data)
        {
            _data = data;

            OnInitialize();
        }

        public void Shutdown()
        {
            OnShutdown();
        }

        protected virtual void OnInitialize()
        { }
        
        protected virtual void OnShutdown()
        { }
    }
}
