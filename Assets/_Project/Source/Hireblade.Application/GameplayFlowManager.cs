using Cysharp.Threading.Tasks;
using Hireblade.Gameplay.System;
using Hireblade.Gameplay.UI.System;
using Hireblade.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hireblade.Application
{
    internal sealed class GameplayFlowManager : IGameFlowStateManager
    {
        public async UniTask EnterAsync(Scene scene)
        {
            GameplaySystem gameplaySystem = SceneQuery.FindInScene<GameplaySystem>(scene);
            GameplayUISystem gameplayUISystem = SceneQuery.FindInScene<GameplayUISystem>(scene);

            Debug.Log("[GameplayFlowManager] Waiting for GameplaySystem before releasing the UI...");

            await gameplaySystem.InitializeAsync();

            gameplayUISystem.Initialize();

            Debug.Log("[GameplayFlowManager] Gameplay and UI ready, control handed to the player.");
        }
    }
}
