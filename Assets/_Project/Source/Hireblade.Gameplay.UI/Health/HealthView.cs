using System;
using DG.Tweening;
using Hireblade.Core.Health;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.UI.Health
{
    internal sealed class HealthView : MonoBehaviour, IPooledObject
    {
        public event Action<HealthView> OnHealthDepleted;

        [Header("Objects")]
        [SerializeField]
        private Billboard billboard;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private ImageFiller imageFiller;

        [Header("Canvas Group Fade Settings")]
        [SerializeField]
        private float canvasGroupFadeDuration = 0.5f;
        [SerializeField]
        private float canvasGroupFadeDelay = 2f;

        private Camera _mainCamera;
        private IHealth _health;

        public string PoolId { get; set; }

        public void Initialize(Camera mainCamera, IHealth health)
        {
            _mainCamera = mainCamera;
            _health = health;

            imageFiller.Initialize(_health.HealthRatio);

            _health.OnHealthChanged += OnHealthChanged;
            _health.OnDepleted += HandleHealthDepleted;

            canvasGroup.alpha = 1f;
        }

        public void Shutdown()
        {
            _health.OnHealthChanged -= OnHealthChanged;
            _health.OnDepleted -= HandleHealthDepleted;
        }

        public void Tick(float deltaTime)
        {
            imageFiller.Tick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            Vector3 worldPosition = transform.position + _mainCamera.transform.forward;

            billboard.LookAt(worldPosition);
        }

        private void OnHealthChanged(float healthRatio)
        {
            imageFiller.UpdateFillAmount(healthRatio);
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
