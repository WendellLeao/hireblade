using UnityEngine;

namespace Hireblade.Gameplay.Damage
{
    internal sealed class Damager : MonoBehaviour, IDamager
    {
        [SerializeField]
        private DamageData data;

        public bool TryApplyDamage(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
            {
                return false;
            }

            damageable.TakeDamage(data);

            return true;
        }
    }
}
