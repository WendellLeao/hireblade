#if UNITY_EDITOR || DEBUG
using Hireblade.Gameplay.Cameras.Manager;
using Hireblade.Gameplay.Characters;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Hireblade.Debugging.Characters
{
    internal sealed class CharacterInitializer : MonoBehaviour
    {
        private void Start()
        {
            ParticleManager particleManager = FindAnyObjectByType<ParticleManager>();
            WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();
            CameraManager cameraManager = FindAnyObjectByType<CameraManager>();

            if (TryGetComponent(out ICharacter character))
            {
                character.Initialize(particleManager.Factory, weaponManager.Factory, cameraManager);
            }
        }
    }
}
#endif
