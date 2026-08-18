using UnityEngine;

namespace Hireblade.Gameplay.Cameras
{
    public interface ICameraProvider
    {
        public Camera MainCamera { get; }
        public IVirtualCamera VirtualCamera { get; }
    }
}
