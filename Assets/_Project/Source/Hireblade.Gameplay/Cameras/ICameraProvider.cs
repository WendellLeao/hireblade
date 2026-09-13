using UnityEngine;

namespace Hireblade.Gameplay.Cameras
{
    public interface ICameraProvider
    {
        Ray ScreenPointToRay(Vector3 pos);
        void SetVirtualCameraTarget(Transform targetTransform);
    }
}
