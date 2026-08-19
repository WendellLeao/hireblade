using UnityEngine;

namespace Hireblade.Application
{
    internal sealed class AppRoot : MonoBehaviour
    {
        [SerializeField]
        private GameFlowManager gameFlowManager;

        private void Start()
        {
            Debug.Log("[AppRoot] Bootstrap complete. Services alive, starting the game flow.");

            gameFlowManager.Initialize();
        }
    }
}
