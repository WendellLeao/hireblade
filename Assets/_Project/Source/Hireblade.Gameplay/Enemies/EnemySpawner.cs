using System;
using Hireblade.Gameplay.Events.Health;
using Hireblade.Gameplay.Shared;

namespace Hireblade.Gameplay.Enemies
{
    internal sealed class EnemySpawner : BasicEntitySpawner<BasicEnemy>
    {
        public event Action<BasicEnemy> OnEnemySpawned;

        protected override BasicEnemy SpawnEntity()
        {
            BasicEnemy enemy = base.SpawnEntity();
            
            enemy.Initialize(ParticleFactory, WeaponFactory);

            EventService.DispatchEvent(new HealthSpawnedEvent(enemy.Health));

            OnEnemySpawned?.Invoke(enemy);

            return enemy;
        }
    }
}
