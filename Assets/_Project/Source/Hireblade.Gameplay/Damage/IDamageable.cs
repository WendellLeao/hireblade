using System;

namespace Hireblade.Gameplay.Damage
{
    public interface IDamageable
    {
        public event Action<DamageData> OnDamageTaken;

        public void TakeDamage(DamageData damageData);
        public void SetIsInvincible(bool isInvincible);
    }
}
