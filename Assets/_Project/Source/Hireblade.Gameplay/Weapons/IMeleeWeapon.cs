
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Weapons
{
    internal interface IMeleeWeapon : IWeapon, IParticleEmitter
    {
        public void SetColliderEnabled(bool isEnabled);
    }
}
