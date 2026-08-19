using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Cameras;
using Hireblade.Gameplay.Particles;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Characters.Manager
{
    internal sealed class CharacterManager : MonoBehaviour
    {
        [SerializeField]
        private CharacterSpawner characterSpawner;

        private readonly List<ICharacter> _characters = new();

        private IPoolingService _poolingService;
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;

            characterSpawner.OnCharacterSpawned += HandleCharacterSpawned;

            characterSpawner.Initialize(_poolingService, _eventService, _particleFactory, _weaponFactory, _cameraProvider);
        }

        public void Tick(float deltaTime)
        {
            foreach (ICharacter character in _characters)
            {
                character.Tick(deltaTime);
            }
        }

        private void OnDestroy()
        {
            characterSpawner.OnCharacterSpawned -= HandleCharacterSpawned;

            characterSpawner.Shutdown();

            for (int i = _characters.Count - 1; i >= 0; i--)
            {
                ShutdownCharacter(_characters[i]);
            }
        }

        private void ShutdownCharacter(ICharacter character)
        {
            character.Shutdown();

            character.OnDied -= HandleCharacterDied;

            _characters.Remove(character);
        }

        private void HandleCharacterSpawned(ICharacter character)
        {
            _characters.Add(character);

            character.OnDied += HandleCharacterDied;
        }

        private void HandleCharacterDied(ICharacter character)
        {
            ShutdownCharacter(character);

            characterSpawner.RespawnEntity(character);
        }
    }
}
