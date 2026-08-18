using Hireblade.Utilities;
using WendellLeao.Pooling;
using UnityEngine;

namespace Hireblade.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = PathUtility.WeaponsMenuPath + "/WeaponData", fileName = "NewWeaponData")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string viewName;
        [SerializeField]
        private float staminaCost;
        [SerializeField]
        private float range;
        [SerializeField]
        private PoolData poolData;
        [SerializeField]
        private MovesetType movesetType;
        
        public string ViewName => viewName;
        public PoolData PoolData => poolData;
        public MovesetType MovesetType => movesetType;
    }
}
