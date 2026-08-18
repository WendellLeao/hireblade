using System;
using Hireblade.Core;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Enemies
{
    public interface IEnemy : IPooledObject
    {
        public event Action<IEnemy> OnDied;

        public IHealth Health { get; }

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
