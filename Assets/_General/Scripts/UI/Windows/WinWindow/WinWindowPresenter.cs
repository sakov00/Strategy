using _Project.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.WinWindow
{
    public class WinWindowPresenter : BaseWindowPresenter
    {
        [Inject] private GameManager _gameManager;
        
        [SerializeField] private WinWindowModel _model;
        [SerializeField] private WinWindowView _view;
        
        protected override BaseWindowModel BaseModel => _model;
        protected override BaseWindowView BaseView => _view;

        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

        protected override void Awake()
        {
            base.Awake();
            IsPaused.Subscribe(PauseGame).AddTo(this);
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(this);
            RestartCommand.Subscribe(_ => RestartOnClick()).AddTo(this);
            ContinueCommand.Subscribe(_ => ContinueOnClick()).AddTo(this);
        }

        public void Initialize(bool isLevelWin)
        {
            
        }
        
        private void HomeOnClick()
        {
        }
        
        private void RestartOnClick()
        {
            WindowsManager.HideWindow<WinWindowPresenter>();
        }
        
        private void ContinueOnClick()
        {
            WindowsManager.HideWindow<WinWindowPresenter>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}