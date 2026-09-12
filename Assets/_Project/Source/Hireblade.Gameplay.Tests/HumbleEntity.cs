using UnityEngine;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Health;

namespace Hireblade.Gameplay.Tests
{
    internal sealed class HumbleEntity : MonoBehaviour
    {
        private HealthController _healthController;
        private DamageController _damageController;

        public void Initialize()
        {
            if (TryGetComponent(out _healthController))
            {
                _healthController.Initialize();
            }

            if (TryGetComponent(out _damageController))
            {
                _damageController.Initialize(_healthController);
            }
        }

        public void Shutdown()
        {
            _damageController.Shutdown();
        }
    }
}
