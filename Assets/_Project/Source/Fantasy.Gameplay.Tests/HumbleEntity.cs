using Fantasy.Core;
using UnityEngine;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HumbleEntity : MonoBehaviour
    {
        private IHealth _health;
        private IDamageable _damageable;

        public void SetUp()
        {
            _health = GetComponent<IHealth>();
            _damageable = GetComponent<IDamageable>();

            _health.SetUp();
            _damageable.SetUp(_health);
        }

        public void Dispose()
        {
            _damageable.Dispose();
        }
    }
}
