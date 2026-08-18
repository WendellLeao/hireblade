using Hireblade.Core;
using UnityEngine;
using Hireblade.Gameplay.Damage;

namespace Hireblade.Gameplay.Tests
{
    internal sealed class HumbleEntity : MonoBehaviour
    {
        private IHealth _health;
        private IDamageable _damageable;

        public void Initialize()
        {
            _health = GetComponent<IHealth>();
            _damageable = GetComponent<IDamageable>();

            _health.Initialize();
            _damageable.Initialize(_health);
        }

        public void Shutdown()
        {
            _damageable.Shutdown();
        }
    }
}
