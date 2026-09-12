using UnityEngine;

namespace Hireblade.Gameplay.Cameras.Manager
{
    public sealed class CameraManager : MonoBehaviour, ICameraProvider
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private VirtualCamera virtualCamera;

        public Ray ScreenPointToRay(Vector3 pos)
        {
            return mainCamera.ScreenPointToRay(pos);
        }
        
        public void SetVirtualCameraTarget(Transform targetTransform)
        {
            virtualCamera.SetTarget(targetTransform);
        }
    }
}
