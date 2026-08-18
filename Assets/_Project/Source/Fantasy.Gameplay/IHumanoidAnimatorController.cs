using Fantasy.Core;

namespace Fantasy.Gameplay
{
    internal interface IHumanoidAnimatorController
    {
        public void SetUp(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
