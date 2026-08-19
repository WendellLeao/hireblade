using Cysharp.Threading.Tasks;
using Hireblade.MainMenu.System;
using Hireblade.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hireblade.Application
{
    internal sealed class MainMenuFlowManager : IGameFlowStateManager
    {
        public UniTask EnterAsync(Scene scene)
        {
            Cursor.lockState = CursorLockMode.None;

            MainMenuSystem mainMenuSystem = SceneQuery.FindInScene<MainMenuSystem>(scene);
            mainMenuSystem.Initialize();

            return UniTask.CompletedTask;
        }
    }
}
