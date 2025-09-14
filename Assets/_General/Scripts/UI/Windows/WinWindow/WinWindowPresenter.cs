using _General.Scripts.AllAppData;
using _Project.Scripts;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.WinWindow
{
    public class WinWindowPresenter : BaseWindowPresenter
    {
        [Inject] private AppData _appData;
        [Inject] private GameManager _gameManager;
        
        [SerializeField] private WinWindowModel _model;
        [SerializeField] private WinWindowView _view;
        
        protected override BaseWindowModel BaseModel => _model;
        protected override BaseWindowView BaseView => _view;

        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

        private bool _isLevelWin;

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
            _isLevelWin = isLevelWin;
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
            if (_isLevelWin)
                _gameManager.StartLevel(_appData.User.CurrentLevel).Forget();
            WindowsManager.HideWindow<WinWindowPresenter>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}