using System;
using Hireblade.Gameplay.Events.Health;
using WendellLeao.Events;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Shared;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Enemies
{
    internal sealed class EnemySpawner : BasicEntitySpawner<IEnemy>
    {
        public event Action<IEnemy> OnEnemySpawned;

        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory)
        {
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.Initialize(poolingService);
        }

        protected override IEnemy SpawnEntity()
        {
            IEnemy enemy = base.SpawnEntity();
            
            enemy.Initialize(_particleFactory, _weaponFactory);

            _eventService.DispatchEvent(new HealthSpawnedEvent(enemy.Health));

            OnEnemySpawned?.Invoke(enemy);

            return enemy;
        }
    }
}
