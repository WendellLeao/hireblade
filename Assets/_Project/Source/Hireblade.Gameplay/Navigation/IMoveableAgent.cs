using UnityEngine;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Navigation
{
    internal interface IMoveableAgent
    {
        public Vector3 Velocity { get; }

        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory);
        public void Tick(float deltaTime);
        public void ResetPath();
    }
}
