#if UNITY_EDITOR || DEBUG
using Hireblade.Gameplay.Enemies;
using Hireblade.Gameplay.Particles.Manager;
using Hireblade.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Hireblade.Debugging
{
    internal sealed class EnemyInitializer : MonoBehaviour
    {
        [SerializeField]
        private BasicEnemy basicEnemy;
        
        private void Start()
        {
            ParticleManager particleManager = FindAnyObjectByType<ParticleManager>();
            WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();
            
            basicEnemy.Initialize(particleManager.Factory, weaponManager.Factory);
        }
    }
}
#endif
