#if UNITY_EDITOR || DEBUG
using UnityEngine;

namespace Hireblade.Debugging.System
{
    internal sealed class DebugSystem : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ToggleCanvasActive();
            }
        }

        private void ToggleCanvasActive()
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }
}
#endif
