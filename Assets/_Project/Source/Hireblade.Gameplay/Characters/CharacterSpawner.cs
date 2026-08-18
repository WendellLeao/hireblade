using System;
using Hireblade.Events.Health;
using WendellLeao.Events;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Characters
{
    internal sealed class CharacterSpawner : BasicEntitySpawner<ICharacter>
    {
        public event Action<ICharacter> OnCharacterSpawned;
        
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;

        public void SetUp(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
            base.SetUp(poolingService);
        }

        protected override ICharacter SpawnEntity()
        {
            ICharacter character = base.SpawnEntity();
            
            character.SetUp(_particleFactory, _weaponFactory, _cameraProvider);
            
            _eventService.DispatchEvent(new HealthSpawnedEvent(character.Health));
            
            OnCharacterSpawned?.Invoke(character);

            return character;
        }
    }
}
