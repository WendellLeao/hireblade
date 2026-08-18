#if UNITY_EDITOR || DEBUG
using Hireblade.Gameplay;
using Hireblade.Gameplay.Cameras.Manager;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Hireblade.Debugging
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
                character.SetUp(particleManager, weaponManager, cameraManager);
            }
        }
    }
}
#endif
