using UnityEngine;

namespace Hireblade.Gameplay.Cameras
{
    public interface ICameraProvider
    {
        public Ray ScreenPointToRay(Vector3 pos);
        public void SetVirtualCameraTarget(Transform targetTransform);
    }
}
