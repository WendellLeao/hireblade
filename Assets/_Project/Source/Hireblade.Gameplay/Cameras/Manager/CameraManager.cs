using UnityEngine;

namespace Hireblade.Gameplay.Cameras.Manager
{
    public sealed class CameraManager : MonoBehaviour, ICameraProvider
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private VirtualCamera virtualCamera;

        public IVirtualCamera VirtualCamera => virtualCamera;
        public Camera MainCamera => mainCamera;
    }
}
