using Hireblade.Core;
using Hireblade.MainMenu.Screens;
using UnityEngine;
using WendellLeao.ServiceLocator;

namespace Hireblade.MainMenu.System
{
    public sealed class MainMenuSystem : MonoBehaviour
    {
        [SerializeField]
        private TitleScreen titleScreen;

        public void Initialize()
        {
            titleScreen.OnPlayRequested += HandlePlayRequested;

            titleScreen.Open();
        }

        private void OnDestroy()
        {
            titleScreen.OnPlayRequested -= HandlePlayRequested;
        }

        private void HandlePlayRequested()
        {
            IGameFlowService gameFlowService = Locator.Get<IGameFlowService>();

            gameFlowService.EnterGameplay();
        }
    }
}
