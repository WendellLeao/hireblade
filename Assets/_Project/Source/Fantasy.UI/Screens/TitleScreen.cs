using UnityEngine;
using UnityEngine.UI;
using WendellLeao.ServiceLocator;
using WendellLeao.Screens;

namespace Fantasy.UI.Screens
{
    internal sealed class TitleScreen : UIScreen
    {
        [Header("Objects")]
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button quitButton;

        [Header("Data")]
        [SerializeField]
        private UIScreenData playConfirmationScreenData;

        protected override void OnSubscribeEvents()
        {
            base.OnSubscribeEvents();

            playButton.onClick.AddListener(HandlePlayButtonClick);
        }

        protected override void OnUnsubscribeEvents()
        {
            base.OnUnsubscribeEvents();

            playButton.onClick.RemoveListener(HandlePlayButtonClick);
        }

        private void HandlePlayButtonClick()
        {
            IScreenService screenService = Locator.Get<IScreenService>();

            screenService.OpenScreenAsync(playConfirmationScreenData);
        }
    }
}
