
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Damage
{
    internal interface IDamageableView
    {
        public void SetUp(IParticleFactory particleFactory, IDamageable damageable);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
