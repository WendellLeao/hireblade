using UnityEngine;

namespace Hireblade.Input
{
    public interface IInputService
    {
        public Vector2 GetPlayerMovement();
        public Vector2 GetMouseDelta();
        public bool GetPlayerJumpedThisFrame();
    }
}
