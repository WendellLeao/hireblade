using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeapon : IPooledObject
    {
        public WeaponData Data { get; }

        public void Initialize(WeaponData data);
        public void Shutdown();
        public void Execute();
        public void FinishExecution();
    }
}
