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
    internal sealed class CharacterSpawner : BasicEntitySpawner<Character>
    {
        public event Action<Character> OnCharacterSpawned;

        private ICameraProvider _cameraProvider;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;

            base.Initialize(poolingService, eventService, particleFactory, weaponFactory);
        }

        protected override Character SpawnEntity()
        {
            Character character = base.SpawnEntity();
            
            character.Initialize(ParticleFactory, WeaponFactory, _cameraProvider);

            EventService.DispatchEvent(new HealthSpawnedEvent(character.Health));
            
            OnCharacterSpawned?.Invoke(character);

            return character;
        }
    }
}
