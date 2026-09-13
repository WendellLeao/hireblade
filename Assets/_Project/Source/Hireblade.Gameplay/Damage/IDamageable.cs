using System;

namespace Hireblade.Gameplay.Damage
{
    public interface IDamageable
    {
        event Action<DamageData> OnDamageTaken;

        void TakeDamage(DamageData damageData);
        void SetIsInvincible(bool isInvincible);
    }
}
