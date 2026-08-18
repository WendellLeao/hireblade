using UnityEngine;

namespace Hireblade.Gameplay
{
    internal interface IMoveableAgent
    {
        public Vector3 Velocity { get; }

        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory);
        public void Tick(float deltaTime);
        public void ResetPath();
    }
}
