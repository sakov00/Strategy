using _Project.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.WinWindow
{
    public class WinWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private WinWindowModel _model;
        
        [Inject] private GameManager _gameManager;
        
        protected override BaseWindowModel BaseModel => _model;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

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
            WindowsManager.HideWindow<WinWindowView>();
        }
        
        private void ContinueOnClick()
        {
            _gameManager.StartLevel(1);
            WindowsManager.HideWindow<WinWindowView>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}