using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Interfaces.Controller
{
    public interface IBuyController
    {
        UniTask TryBuy();
    }
}