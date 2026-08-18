using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IDamager
    {
        public bool TryApplyDamage(Collider other);
    }
}
