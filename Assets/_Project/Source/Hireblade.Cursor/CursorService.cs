using UnityEngine;
using WendellLeao.ServiceLocator;

namespace Hireblade.Cursor
{
    [DisallowMultipleComponent]
    public sealed class CursorService : MonoBehaviour, ICursorService
    {
        public void SetLockState(CursorLockMode lockState)
        {
            UnityEngine.Cursor.lockState = lockState;
        }

        private void Awake()
        {
            Locator.Register<ICursorService>(this);
        }

        private void OnDestroy()
        {
            Locator.Unregister<ICursorService>();
        }
    }
}
