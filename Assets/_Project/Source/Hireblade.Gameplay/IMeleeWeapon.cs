namespace Hireblade.Gameplay
{
    internal interface IMeleeWeapon : IWeapon, IParticleEmitter
    {
        public void SetColliderEnabled(bool isEnabled);
    }
}
