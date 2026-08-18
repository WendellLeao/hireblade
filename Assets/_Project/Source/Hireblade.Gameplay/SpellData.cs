using Hireblade.Utilities;
using WendellLeao.Pooling;
using UnityEngine;

namespace Hireblade.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.SpellsMenuPath + "/SpellData", fileName = "NewSpellData")]
    public sealed class SpellData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private PoolData poolData;

        public PoolData PoolData => poolData;
    }
}
