using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Hireblade.Application
{
    internal interface IGameFlowStateManager
    {
        UniTask EnterAsync(Scene scene);
    }
}
