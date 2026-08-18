using UnityEngine;
using WendellLeao.ServiceLocator;

namespace Fantasy.Input
{
    /// <summary>
    /// The InputService provides the abstraction <see cref="IInputService"/> to expose all the players inputs.
    /// <seealso cref="Locator"/>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InputService : MonoBehaviour, IInputService
    {
        private Inputs _inputs;
        private Inputs.LandMapActions _landActions;

        public Vector2 GetPlayerMovement()
        {
            return _landActions.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return _landActions.Look.ReadValue<Vector2>();
        }

        public bool GetPlayerJumpedThisFrame()
        {
            return _landActions.Jump.triggered;
        }

        private void Awake()
        {
            Locator.Register<IInputService>(this);

            _inputs = new Inputs();

            _landActions = _inputs.LandMap;

            _inputs.Enable();
        }

        private void OnDestroy()
        {
            Locator.Unregister<IInputService>();

            _inputs.Disable();
        }
    }
}
