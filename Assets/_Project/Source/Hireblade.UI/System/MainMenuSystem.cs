using Cysharp.Threading.Tasks;
using Hireblade.Core;
using UnityEngine;
using WendellLeao.ServiceLocator;
using WendellLeao.Screens;

namespace Hireblade.UI.System
{
    public sealed class MainMenuSystem : MonoBehaviour, IInitializableAsync
    {
        [Header("Data")]
        [SerializeField]
        private UIScreenData titleScreenData;

        public async UniTask InitializeAsync()
        {
            Cursor.lockState = CursorLockMode.None;

            IScreenService screenService = Locator.Get<IScreenService>();

            await screenService.OpenScreenAsync(titleScreenData);
        }
    }
}
