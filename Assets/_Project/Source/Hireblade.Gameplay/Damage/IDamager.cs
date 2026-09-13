using UnityEngine;

namespace Hireblade.Gameplay.Damage
{
    internal interface IDamager
    {
        bool TryApplyDamage(Collider other);
    }
}
