using Fantasy.UI.Health.Manager;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using WendellLeao.ServiceLocator;

namespace Fantasy.UI.System
{
    internal sealed class UISystem : MonoBehaviour
    {
        [SerializeField]
        private HealthViewManager healthViewsManager;

        private void Awake()
        {
            IPoolingService poolingService = Locator.Get<IPoolingService>();
            IEventService eventService = Locator.Get<IEventService>();

            // TODO: camera service
            healthViewsManager.SetUp(Camera.main, poolingService, eventService);
        }
    }
}
