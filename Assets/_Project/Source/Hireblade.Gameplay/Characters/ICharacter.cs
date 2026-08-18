using System;
using Hireblade.Core;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Characters
{
    public interface ICharacter : IPooledObject
    {
        public event Action<ICharacter> OnDied;

        public IHealth Health { get; }

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory,
            ICameraProvider cameraProvider);
        public void Shutdown();
        public void Tick(float deltaTime);
    }
}
