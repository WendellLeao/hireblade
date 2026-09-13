using UnityEngine;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Weapons
{
    internal abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        private WeaponData _data;
        private IParticleFactory _particleFactory;

        public string PoolId { get; set; }
        public WeaponData Data => _data;
        protected IParticleFactory ParticleFactory => _particleFactory;

        public abstract void Execute();

        public abstract void FinishExecution();

        public void Initialize(WeaponData data, IParticleFactory particleFactory)
        {
            _data = data;
            _particleFactory = particleFactory;

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
