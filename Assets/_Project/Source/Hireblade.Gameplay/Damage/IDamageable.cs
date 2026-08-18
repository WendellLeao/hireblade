using System;
using Hireblade.Core.Health;

namespace Hireblade.Gameplay.Damage
{
    public interface IDamageable
    {
        public event Action<DamageData> OnDamageTaken;

        public void Initialize(IHealth health);
        public void Shutdown();
        public void Tick(float deltaTime);
        public void TakeDamage(DamageData damageData);
        public void SetIsInvincible(bool isInvincible);
    }
}
