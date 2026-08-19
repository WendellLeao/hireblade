using Cysharp.Threading.Tasks;

namespace Hireblade.Core
{
    public interface IInitializableAsync
    {
        UniTask InitializeAsync();
    }
}
