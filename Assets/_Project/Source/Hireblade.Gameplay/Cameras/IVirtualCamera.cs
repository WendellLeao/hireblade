using UnityEngine;

namespace Hireblade.Gameplay.Cameras
{
    public interface IVirtualCamera
    {
        public void SetTarget(Transform targetTransform);
    }
}
