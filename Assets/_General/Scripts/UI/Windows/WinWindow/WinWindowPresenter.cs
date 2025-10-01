using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.UI.Windows.BaseWindow;
using _Project.Scripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        
        public override BaseWindowModel Model => _model;
        public override BaseWindowView View => _view;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

        private bool _isLevelWin;

        public override void Initialize()
        {
            base.Initialize();
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(Disposables);
            RestartCommand.Subscribe(_ => RestartOnClick()).AddTo(Disposables);
            ContinueCommand.Subscribe(_ => ContinueOnClick()).AddTo(Disposables);
        }

        public void SetWindowData(bool isLevelWin)
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
    }
}