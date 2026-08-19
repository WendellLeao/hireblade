using System;
using UnityEngine;
using UnityEngine.UI;
using WendellLeao.Screens;

namespace Hireblade.MainMenu.Screens
{
    internal sealed class TitleScreen : UIScreen
    {
        public event Action OnPlayRequested;

        [Header("Objects")]
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button quitButton;

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
            OnPlayRequested?.Invoke();
        }
    }
}
