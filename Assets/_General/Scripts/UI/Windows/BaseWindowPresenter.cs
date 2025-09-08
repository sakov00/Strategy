using _General.Scripts._VContainer;
using _General.Scripts.Enums;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows
{
    public abstract class BaseWindowPresenter : MonoBehaviour
    {
        [Inject] protected WindowsManager WindowsManager { get; private set; }
        
        protected abstract BaseWindowModel BaseModel { get; }
        protected abstract BaseWindowView BaseView { get; }
        public IReactiveProperty<bool> IsPaused => BaseModel.IsPausedReactive;
        public WindowType WindowType => BaseModel.WindowType;

        protected virtual void Awake()
        {
            InjectManager.Inject(this);
            IsPaused.Subscribe(PauseGame).AddTo(this);
        }

        public Tween Hide() => BaseView.Hide();
        public Tween Show() => BaseView.Show();

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}