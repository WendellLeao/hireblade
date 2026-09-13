using Hireblade.Gameplay.Damage;
using UnityEngine;

namespace Hireblade.Gameplay.Spells
{
    internal sealed class ElementalBall : BaseSpell
    {
        [Header("Elemental Ball")]
        [SerializeField]
        private ApplyForwardForce applyForwardForce;
        [SerializeField]
        private Damager damager;

        protected override void OnTriggerEnter(Collider other)
        {
            damager.TryApplyDamage(other);

            base.OnTriggerEnter(other);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            applyForwardForce.Initialize();
        }
    }
}
