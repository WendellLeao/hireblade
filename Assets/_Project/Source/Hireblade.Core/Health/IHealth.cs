using System;

namespace Hireblade.Core.Health
{
    public interface IHealth
    {
        event Action<float> OnHealthChanged;
        event Action OnDepleted;

        float HealthRatio { get; }

        void IncrementHealth(float amount);
        void DecrementHealth(float amount);
    }
}
