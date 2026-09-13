using UnityEngine;

namespace Hireblade.Gameplay.Navigation
{
    internal interface IMoveableAgent
    {
        Vector3 Velocity { get; }

        void ResetPath();
    }
}
