using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Events;
using WendellLeao.Pooling;

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

        public void SetUp(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;

            characterSpawner.OnCharacterSpawned += HandleCharacterSpawned;

            characterSpawner.SetUp(_poolingService, _eventService, _particleFactory, _weaponFactory, _cameraProvider);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (ICharacter character in _characters)
            {
                character.Tick(deltaTime);
            }
        }

        private void OnDestroy()
        {
            characterSpawner.OnCharacterSpawned -= HandleCharacterSpawned;

            characterSpawner.Dispose();

            for (int i = _characters.Count - 1; i >= 0; i--)
            {
                DisposeCharacter(_characters[i]);
            }
        }

        private void DisposeCharacter(ICharacter character)
        {
            character.Dispose();

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
            DisposeCharacter(character);

            characterSpawner.RespawnEntity(character);
        }
    }
}
