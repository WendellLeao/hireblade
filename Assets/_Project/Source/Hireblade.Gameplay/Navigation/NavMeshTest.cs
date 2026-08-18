using UnityEngine;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Navigation
{
    internal sealed class NavMeshTest : MonoBehaviour, IMoveableAgent
    {
        public Vector3 Velocity => Vector3.zero;

        public void Initialize(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        { }

        public void Tick(float deltaTime)
        { }

        public void ResetPath()
        { }
    }
}
