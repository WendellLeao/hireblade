using System;
using Fantasy.Core;
using WendellLeao.Pooling;

namespace Fantasy.Gameplay
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
