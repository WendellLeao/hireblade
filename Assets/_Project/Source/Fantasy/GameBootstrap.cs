using UnityEngine;

namespace Fantasy
{
    [DisallowMultipleComponent]
    internal sealed class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            transform.SetParent(p: null);

            DontDestroyOnLoad(gameObject);
        }
    }
}
