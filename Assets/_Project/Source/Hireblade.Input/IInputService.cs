using UnityEngine;

namespace Hireblade.Input
{
    public interface IInputService
    {
        Vector2 GetPlayerMovement();
        Vector2 GetMouseDelta();
        bool GetPlayerJumpedThisFrame();
    }
}
