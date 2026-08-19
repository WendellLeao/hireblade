using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Enemies.Manager
{
    internal sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner enemySpawner;

        private readonly List<IEnemy> _enemies = new();

        private IPoolingService _poolingService;
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;

            enemySpawner.OnEnemySpawned += HandleEnemySpawned;

            enemySpawner.Initialize(_poolingService, _eventService, _particleFactory, _weaponFactory);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (IEnemy enemy in _enemies)
            {
                enemy.Tick(deltaTime);
            }
        }

        private void OnDestroy()
        {
            enemySpawner.OnEnemySpawned -= HandleEnemySpawned;

            enemySpawner.Shutdown();

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                ShutdownEnemy(_enemies[i]);
            }
        }

        private void ShutdownEnemy(IEnemy enemy)
        {
            enemy.Shutdown();

            enemy.OnDied -= HandleEnemyDied;

            _enemies.Remove(enemy);
        }

        private void HandleEnemySpawned(IEnemy enemy)
        {
            _enemies.Add(enemy);

            enemy.OnDied += HandleEnemyDied;
        }

        private void HandleEnemyDied(IEnemy enemy)
        {
            ShutdownEnemy(enemy);

            enemySpawner.RespawnEntity(enemy);
        }
    }
}
