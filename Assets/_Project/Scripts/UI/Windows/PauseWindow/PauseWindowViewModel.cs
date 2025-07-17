using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Services;
using _Project.Scripts.Windows;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private PauseWindowModel model;
        [Inject] private GameManager _gameManager;
        
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
        }
        
        private void RestartOnClick()
        {
            _gameManager.StartLevel(0);
            WindowsManager.HideWindow<PauseWindowView>();
        }
        
        private void ContinueOnClick()
        {
            WindowsManager.HideWindow<PauseWindowView>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}