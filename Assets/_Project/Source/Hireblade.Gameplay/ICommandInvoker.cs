namespace Hireblade.Gameplay
{
    internal interface ICommandInvoker
    {
        public void SetUp(IWeaponHolder weaponHolder);
        public void Dispose();
        public void Tick(float deltaTime);
    }
}
