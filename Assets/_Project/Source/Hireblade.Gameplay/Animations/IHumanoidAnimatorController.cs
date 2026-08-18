using Hireblade.Core;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Animations
{
    internal interface IHumanoidAnimatorController
    {
        public void SetUp(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
