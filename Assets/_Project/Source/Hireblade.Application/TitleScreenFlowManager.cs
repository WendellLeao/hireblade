using Cysharp.Threading.Tasks;
using Hireblade.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using WendellLeao.Screens;

namespace Hireblade.Application
{
    internal sealed class TitleScreenFlowManager : IGameFlowStateManager
    {
        public UniTask EnterAsync(Scene scene)
        {
            Cursor.lockState = CursorLockMode.None;

            UIScreen titleScreen = SceneQuery.FindInScene<UIScreen>(scene);
            titleScreen.Open();

            return UniTask.CompletedTask;
        }
    }
}
