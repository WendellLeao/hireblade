using UnityEngine;

namespace Fantasy.Gameplay.Navigation
{
    internal sealed class NavMeshTest : MonoBehaviour, IMoveableAgent
    {
        public Vector3 Velocity => Vector3.zero;

        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        { }

        public void Tick(float deltaTime)
        { }

        public void ResetPath()
        { }
    }
}
