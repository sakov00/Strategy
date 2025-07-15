using _Project.Scripts.Windows;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private PauseWindowModel model;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();
        
        public IReactiveProperty<bool> IsPaused => model.IsPausedReactive;

        private void Awake()
        {
            IsPaused.Subscribe(PauseGame).AddTo(this);
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(this);
            RestartCommand.Subscribe(_ => RestartOnClick()).AddTo(this);
            ContinueCommand.Subscribe(_ => ContinueOnClick()).AddTo(this);
        }
        
        private void HomeOnClick()
        {
            model.IsPaused = false;
            WindowsManager.HideWindow<PauseWindowView>();
        }
        
        private void RestartOnClick()
        {
            model.IsPaused = false;
        }
        
        private void ContinueOnClick()
        {
            model.IsPaused = false;
        }

        private void PauseGame(bool isPaused)
        {
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}