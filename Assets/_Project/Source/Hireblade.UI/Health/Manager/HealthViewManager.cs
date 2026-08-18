using System.Collections.Generic;
using Hireblade.Core;
using Hireblade.Events.Health;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;

namespace Hireblade.UI.Health.Manager
{
    internal sealed class HealthViewManager : MonoBehaviour
    {
        [SerializeField]
        private PoolData healthViewPoolData;

        private readonly List<HealthView> _healthViews = new();

        private Camera _mainCamera;
        private IPoolingService _poolingService;
        private IEventService _eventService;

        public void Initialize(Camera mainCamera, IPoolingService poolingService, IEventService eventService)
        {
            _mainCamera = mainCamera;
            _poolingService = poolingService;
            _eventService = eventService;

            _eventService.AddEventListener<HealthSpawnedEvent>(HandleHealthSpawned);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (HealthView healthView in _healthViews)
            {
                healthView.Tick(deltaTime);
            }
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;

            foreach (HealthView healthView in _healthViews)
            {
                healthView.LateTick(deltaTime);
            }
        }

        private void OnDestroy()
        {
            _eventService.RemoveEventListener<HealthSpawnedEvent>(HandleHealthSpawned);

            for (int i = _healthViews.Count - 1; i >= 0; i--)
            {
                DisposeHealthView(_healthViews[i]);
            }
        }

        private void DisposeHealthView(HealthView healthView)
        {
            healthView.Shutdown();

            healthView.OnHealthDepleted -= HandleHealthDepleted;

            _healthViews.Remove(healthView);

            _poolingService.ReleaseObjectToPool(healthView);
        }

        private void HandleHealthSpawned(HealthSpawnedEvent healthSpawnedEvent)
        {
            IHealth health = healthSpawnedEvent.Health;

            if (health is not IHealthBarAnchor healthBarAnchor)
            {
                return;
            }

            if (!_poolingService.TryGetObjectFromPool(healthViewPoolData.Id, healthBarAnchor.HealthBarParent, out HealthView healthView))
            {
                return;
            }

            _healthViews.Add(healthView);

            healthView.OnHealthDepleted += HandleHealthDepleted;

            healthView.Initialize(_mainCamera, health);
        }

        private void HandleHealthDepleted(HealthView healthView)
        {
            DisposeHealthView(healthView);
        }
    }
}
