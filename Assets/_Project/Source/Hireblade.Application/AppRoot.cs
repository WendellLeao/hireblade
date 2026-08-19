using UnityEngine;

namespace Hireblade.Application
{
    internal sealed class AppRoot : MonoBehaviour
    {
        [SerializeField]
        private GameFlowManager gameFlowManager;

        private void Start()
        {
            gameFlowManager.Initialize();
        }
    }
}
