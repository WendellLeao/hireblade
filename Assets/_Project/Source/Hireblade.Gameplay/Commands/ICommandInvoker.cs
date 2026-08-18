
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Commands
{
    internal interface ICommandInvoker
    {
        public void Initialize(IWeaponHolder weaponHolder);
        public void Shutdown();
        public void Tick(float deltaTime);
    }
}
