using UnityEngine;

namespace Fantasy.Input
{
    public interface IInputService
    {
        public Vector2 GetPlayerMovement();
        public Vector2 GetMouseDelta();
        public bool GetPlayerJumpedThisFrame();
    }
}
