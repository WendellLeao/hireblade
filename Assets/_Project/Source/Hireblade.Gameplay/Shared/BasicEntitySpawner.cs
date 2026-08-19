using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Shared
{
    internal abstract class BasicEntitySpawner<TEntity> : MonoBehaviour where TEntity : IPooledObject
    {
        [Header("Objects")]
        [SerializeField]
        private PoolData poolData;
        [SerializeField]
        private Transform spawnPoint;

        [Header("Settings")]
        [SerializeField]
        private float releaseDelay = 3f;
        [SerializeField]
        private float respawnDelay = 1f;

        private CancellationTokenSource _releaseEntityCts;
        private IPoolingService _poolingService;

        public void Initialize(IPoolingService poolingService)
        {
            _poolingService = poolingService;

            SpawnEntity();
        }

        public void Shutdown()
        {
            ShutdownCancellationTokenSource();
        }

        public void RespawnEntity(TEntity entity)
        {
            _releaseEntityCts?.Cancel();
            _releaseEntityCts = new CancellationTokenSource();

            RespawnEntityAsync(entity, _releaseEntityCts.Token).Forget();
        }

        protected virtual TEntity SpawnEntity()
        {
            if (!_poolingService.TryGetObjectFromPool(poolData.Id, spawnPoint, out IPooledObject pooledObject))
            {
                return default;
            }

            return (TEntity)pooledObject;
        }

        private async UniTask<TEntity> RespawnEntityAsync(TEntity entity, CancellationToken token)
        {
            try
            {
                await ReleaseEntity(entity, releaseDelay, token);

                await UniTask.Delay(TimeSpan.FromSeconds(respawnDelay), cancellationToken: token);

                return SpawnEntity();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                _releaseEntityCts?.Dispose();
                _releaseEntityCts = null;
            }

            return default;
        }

        private async UniTask ReleaseEntity(TEntity entity, float delay, CancellationToken token)
        {
            if (entity is not IPooledObject pooledObject)
            {
                throw new InvalidOperationException($"Entity of type {entity.GetType().Name} does not implement {nameof(IPooledObject)}!");
            }

            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

            _poolingService.ReleaseObjectToPool(pooledObject);
        }
        
        private void ShutdownCancellationTokenSource()
        {
            _releaseEntityCts?.Cancel();
            _releaseEntityCts?.Dispose();
            _releaseEntityCts = null;
        }
    }
}
