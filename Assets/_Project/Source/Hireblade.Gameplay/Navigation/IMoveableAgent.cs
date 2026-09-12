using UnityEngine;

namespace Hireblade.Gameplay.Navigation
{
    internal interface IMoveableAgent
    {
        public Vector3 Velocity { get; }

        public void ResetPath();
    }
}
