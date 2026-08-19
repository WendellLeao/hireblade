using Cysharp.Threading.Tasks;
using Hireblade.Cursor;
using Hireblade.MainMenu.System;
using Hireblade.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using WendellLeao.ServiceLocator;

namespace Hireblade.Application
{
    internal sealed class MainMenuFlowManager : IGameFlowStateManager
    {
        public UniTask EnterAsync(Scene scene)
        {
            ICursorService cursorService = Locator.Get<ICursorService>();
            cursorService.SetLockState(CursorLockMode.None);

            MainMenuSystem mainMenuSystem = SceneQuery.FindInScene<MainMenuSystem>(scene);
            mainMenuSystem.Initialize();

            return UniTask.CompletedTask;
        }
    }
}
