using UnityEngine;

namespace Hireblade.Gameplay.Damage
{
    internal interface IDamager
    {
        public bool TryApplyDamage(Collider other);
    }
}
