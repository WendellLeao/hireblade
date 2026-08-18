using System;
using Hireblade.Core.Health;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Enemies
{
    public interface IEnemy : IPooledObject
    {
        public event Action<IEnemy> OnDied;

        public IHealth Health { get; }

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory);
        public void Shutdown();
        public void Tick(float deltaTime);
    }
}
