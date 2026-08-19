using UnityEngine;

namespace Hireblade.Gameplay.UI.Health
{
    internal sealed class Billboard : MonoBehaviour
    {
        public void LookAt(Vector3 worldPosition)
        {
            transform.LookAt(worldPosition);
        }
    }
}
