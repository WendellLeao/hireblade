using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ApplyForwardForce : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rigidBody;
        [SerializeField]
        private float force = 10f;

        public void SetUp()
        {
            rigidBody.AddRelativeForce(transform.forward * force, ForceMode.Impulse);
        }
    }
}
