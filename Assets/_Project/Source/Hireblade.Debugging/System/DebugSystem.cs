#if UNITY_EDITOR || DEBUG
using Cysharp.Threading.Tasks;
using Hireblade.Gameplay.System;
using UnityEngine;

namespace Hireblade.Debugging.System
{
    internal sealed class DebugSystem : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        private void Awake()
        {
            GameplaySystem gameplaySystem = FindAnyObjectByType<GameplaySystem>();

            if (gameplaySystem != null)
            {
                gameplaySystem.InitializeAsync().Forget();
            }
        }

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
