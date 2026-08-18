using Hireblade.Core.Health;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Navigation;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Animations
{
    internal interface IHumanoidAnimatorController
    {
        public void Initialize(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
        public void Shutdown();
        public void Tick(float deltaTime);
    }
}
