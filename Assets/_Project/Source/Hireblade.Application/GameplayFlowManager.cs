using Cysharp.Threading.Tasks;
using Hireblade.Gameplay.System;
using Hireblade.Gameplay.UI.System;
using Hireblade.Utilities;
using UnityEngine.SceneManagement;

namespace Hireblade.Application
{
    internal sealed class GameplayFlowManager : IGameFlowStateManager
    {
        public async UniTask EnterAsync(Scene scene)
        {
            GameplaySystem gameplaySystem = SceneQuery.FindInScene<GameplaySystem>(scene);
            UISystem uiSystem = SceneQuery.FindInScene<UISystem>(scene);

            await gameplaySystem.InitializeAsync();

            uiSystem.Initialize();
        }
    }
}
