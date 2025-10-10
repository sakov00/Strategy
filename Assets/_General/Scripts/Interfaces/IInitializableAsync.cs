using Cysharp.Threading.Tasks;

namespace _General.Scripts.Interfaces
{
    public interface IInitializableAsync
    {
        UniTask InitializeAsync();
    }
}