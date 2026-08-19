using Cysharp.Threading.Tasks;
using Hireblade.UI.System;
using Hireblade.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hireblade.Application
{
    internal sealed class MainMenuFlowManager : IGameFlowStateManager
    {
        public async UniTask EnterAsync(Scene scene)
        {
            MainMenuSystem mainMenuSystem = SceneQuery.FindInScene<MainMenuSystem>(scene);

            await mainMenuSystem.InitializeAsync();

            Debug.Log("[MainMenuFlowManager] Main menu ready.");
        }
    }
}
