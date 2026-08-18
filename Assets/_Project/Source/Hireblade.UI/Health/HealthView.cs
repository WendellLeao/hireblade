using System;
using DG.Tweening;
using Hireblade.Core;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.UI.Health
{
    internal sealed class HealthView : MonoBehaviour, IPooledObject
    {
        public event Action<HealthView> OnHealthDepleted;

        [Header("Objects")]
        [SerializeField]
        private Billboard billboard;
        [SerializeField]
        private CanvasGroup canvasGroup;

        [Header("Canvas Group Fade Settings")]
        [SerializeField]
        private float canvasGroupFadeDuration = 0.5f;
        [SerializeField]
        private float canvasGroupFadeDelay = 2f;

        private Camera _mainCamera;
        private IHealth _health;
        private ImageFiller _imageFiller;

        public string PoolId { get; set; }

        public void SetUp(Camera mainCamera, IHealth health)
        {
            _mainCamera = mainCamera;
            _health = health;

            _imageFiller = GetComponent<ImageFiller>();

            _imageFiller.SetUp(_health.HealthRatio);

            _health.OnHealthChanged += HandleHealthChanged;
            _health.OnDepleted += HandleHealthDepleted;

            canvasGroup.alpha = 1f;
        }

        public void Dispose()
        {
            _health.OnHealthChanged -= HandleHealthChanged;
            _health.OnDepleted -= HandleHealthDepleted;
        }

        public void Tick(float deltaTime)
        {
            _imageFiller.Tick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            Vector3 worldPosition = transform.position + _mainCamera.transform.forward;

            billboard.LookAt(worldPosition);
        }

        private void HandleHealthChanged(float healthRatio)
        {
            _imageFiller.UpdateFillAmount(healthRatio);
        }

        private void HandleHealthDepleted()
        {
            canvasGroup
                .DOFade(endValue: 0f, canvasGroupFadeDuration)
                .SetDelay(canvasGroupFadeDelay)
                .OnComplete(DispatchHealthDepletedEvent);
        }

        private void DispatchHealthDepletedEvent()
        {
            OnHealthDepleted?.Invoke(this);
        }
    }
}
