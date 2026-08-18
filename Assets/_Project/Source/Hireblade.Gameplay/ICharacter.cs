using System;
using Hireblade.Core;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay
{
    public interface ICharacter : IPooledObject
    {
        public event Action<ICharacter> OnDied;

        public IHealth Health { get; }

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory,
            ICameraProvider cameraProvider);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
