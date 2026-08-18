using UnityEngine;

namespace Hireblade.Gameplay
{
    internal interface IDamager
    {
        public bool TryApplyDamage(Collider other);
    }
}
