using UnityEngine;
using WendellLeao.ServiceLocator;
using WendellLeao.Screens;

namespace Hireblade.UI
{
    // TODO: this is temporary. The title screen must be loaded after the startup scene finishes its processes.
    internal sealed class LandingPageController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private UIScreenData titleScreenData;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;

            IScreenService screenService = Locator.Get<IScreenService>();

            screenService.OpenScreenAsync(titleScreenData);
        }
    }
}
