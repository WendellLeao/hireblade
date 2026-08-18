
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Commands
{
    internal interface ICommandInvoker
    {
        public void SetUp(IWeaponHolder weaponHolder);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
