using _General.Scripts.Enums;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows
{
    public abstract class BaseWindowViewModel : MonoBehaviour
    {
        [Inject] protected WindowsManager WindowsManager { get; private set; }
        
        protected abstract BaseWindowModel BaseModel { get; }

        public IReactiveProperty<bool> IsPaused => BaseModel.IsPausedReactive;
        public IReactiveProperty<WindowType> WindowType => BaseModel.WindowTypeReactive;

        protected virtual void Awake()
        {
            IsPaused.Subscribe(PauseGame).AddTo(this);
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}