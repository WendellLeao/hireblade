#if UNITY_EDITOR || DEBUG
using Hireblade.Gameplay.Cameras.Manager;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Weapons.Manager;
using UnityEngine;
using Hireblade.Gameplay.Characters;

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
                character.SetUp(particleManager.Factory, weaponManager.Factory, cameraManager);
            }
        }
    }
}
#endif
