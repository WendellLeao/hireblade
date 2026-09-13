using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Weapons
{
    public interface IWeapon : IPooledObject
    {
        WeaponData Data { get; }

        void Execute();
        void FinishExecution();
    }
}
