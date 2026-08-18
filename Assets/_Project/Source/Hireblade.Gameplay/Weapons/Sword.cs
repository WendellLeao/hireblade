using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Damage;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Weapons
{
    internal sealed class Sword : MonoBehaviour, IMeleeWeapon
    {
        [Header("Components")]
        [SerializeField]
        private CapsuleCollider capsuleCollider;

        [Header("Data")]
        [SerializeField]
        private PoolData bloodParticlesPoolData;

        private IParticleFactory _particleFactory;
        private IDamager _damager;
        private WeaponData _data;
        private bool _isEnabled;

        public WeaponData Data => _data;
        public string PoolId { get; set; }

        public void SetUp(WeaponData data)
        {
            _data = data;

            _damager = GetComponent<IDamager>();

            SetColliderEnabled(false);

            _isEnabled = true;
        }

        public void Dispose()
        {
            _isEnabled = false;
        }

        public void Execute()
        {
            SetColliderEnabled(false);
        }

        public void FinishExecution()
        {
            SetColliderEnabled(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled)
            {
                return;
            }

            _damager.TryApplyDamage(other);

            _particleFactory.EmitParticle(bloodParticlesPoolData, transform.position, Quaternion.identity);
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            capsuleCollider.enabled = isEnabled;
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
