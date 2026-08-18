using System;

namespace Hireblade.Core.Health
{
    public interface IHealth
    {
        public event Action<float> OnHealthChanged;
        public event Action OnDepleted;

        public float HealthRatio { get; }

        public void Initialize();
        public void IncrementHealth(float amount);
        public void DecrementHealth(float amount);
    }
}
