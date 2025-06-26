using Cysharp.Threading.Tasks;
using UnityEditor.Rendering;

namespace _Project.Scripts.Interfaces
{
    public interface IBuyController
    {
        UniTask TryBuy();
    }
}