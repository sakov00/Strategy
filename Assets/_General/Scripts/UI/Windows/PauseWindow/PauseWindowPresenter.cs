using _General.Scripts.AllAppData;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows.BaseWindow;
using _Project.Scripts;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowPresenter : BaseWindowPresenter
    {
        [Inject] private AppData _appData;
        [Inject] private GameManager _gameManager;
        [Inject] private SaveLoadLevelService _saveLoadLevelService;
        
        [SerializeField] private PauseWindowModel _model;
        [SerializeField] private PauseWindowView _view;
        
        public override BaseWindowModel Model => _model;
        public override BaseWindowView View => _view;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

        public override void Initialize()
        {
            base.Initialize();
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(Disposables);
            RestartCommand.Subscribe(_ => RestartOnClick().Forget()).AddTo(Disposables);
            ContinueCommand.Subscribe(_ => ContinueOnClick()).AddTo(Disposables);
        }
        
        private void HomeOnClick()
        {
        }
        
        private async UniTask RestartOnClick()
        {
            WindowsManager.HideWindow<PauseWindowPresenter>();
            _saveLoadLevelService.RemoveProgress(_appData.User.CurrentLevel);
            await _gameManager.StartLevel(_appData.User.CurrentLevel);
        }
        
        private void ContinueOnClick()
        {
            WindowsManager.HideWindow<PauseWindowPresenter>();
        }
    }
}