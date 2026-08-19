using System;
using Hireblade.Gameplay.Events.Health;
using WendellLeao.Events;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Shared;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Characters
{
    internal sealed class CharacterSpawner : BasicEntitySpawner<ICharacter>
    {
        public event Action<ICharacter> OnCharacterSpawned;
        
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
            base.Initialize(poolingService);
        }

        protected override ICharacter SpawnEntity()
        {
            ICharacter character = base.SpawnEntity();
            
            character.Initialize(_particleFactory, _weaponFactory, _cameraProvider);
            
            _eventService.DispatchEvent(new HealthSpawnedEvent(character.Health));
            
            OnCharacterSpawned?.Invoke(character);

            return character;
        }
    }
}
