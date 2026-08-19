using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hireblade.Utilities
{
    public static class SceneQuery
    {
        public static T FindInScene<T>(Scene scene) where T : Component
        {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            {
                T component = rootGameObject.GetComponentInChildren<T>(true);

                if (component != null)
                {
                    return component;
                }
            }

            return null;
        }
    }
}
