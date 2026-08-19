using Hireblade.Gameplay.UI.Health.Manager;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using WendellLeao.ServiceLocator;

namespace Hireblade.Gameplay.UI.System
{
    public sealed class UISystem : MonoBehaviour
    {
        [SerializeField]
        private HealthViewManager healthViewsManager;

        private bool _isInitialized;

        public void Initialize()
        {
            IPoolingService poolingService = Locator.Get<IPoolingService>();
            IEventService eventService = Locator.Get<IEventService>();

            // TODO: camera service
            healthViewsManager.Initialize(Camera.main, poolingService, eventService);

            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            healthViewsManager.Tick(Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            healthViewsManager.LateTick(Time.deltaTime);
        }
    }
}
