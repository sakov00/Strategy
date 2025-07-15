using UnityEngine;
using VContainer;

namespace _Project.Scripts.UI.Windows
{
    public abstract class BaseWindowViewModel : MonoBehaviour
    {
        [Inject] protected WindowsManager WindowsManager { get; private set; }

    }
}