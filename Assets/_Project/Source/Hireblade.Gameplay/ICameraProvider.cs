using UnityEngine;

namespace Hireblade.Gameplay
{
    public interface ICameraProvider
    {
        public Camera MainCamera { get; }
        public IVirtualCamera VirtualCamera { get; }
    }
}
