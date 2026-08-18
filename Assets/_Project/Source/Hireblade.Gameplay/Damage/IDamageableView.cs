
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Damage
{
    internal interface IDamageableView
    {
        public void Initialize(IParticleFactory particleFactory, IDamageable damageable);
        public void Shutdown();
        public void Tick(float deltaTime);
    }
}
