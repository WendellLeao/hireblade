using WendellLeao.Pooling;

namespace Hireblade.Gameplay
{
    public interface IWeapon : IPooledObject
    {
        public WeaponData Data { get; }

        public void SetUp(WeaponData data);
        public void Dispose();
        public void Execute();
        public void FinishExecution();
    }
}
