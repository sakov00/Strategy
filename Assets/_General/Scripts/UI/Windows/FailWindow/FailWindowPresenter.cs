using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.UI.Windows.BaseWindow;
using _Project.Scripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.FailWindow
{
    public class FailWindowPresenter : BaseWindowPresenter
    {
        [Inject] private AppData _appData;
        [Inject] private GameManager _gameManager;
        
        [SerializeField] private FailWindowModel _model;
        [SerializeField] private FailWindowView _view;
        public override BaseWindowModel Model => _model;
        public override BaseWindowView View => _view;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        
        public override void Initialize()
        {
            base.Initialize();
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(Disposables);
            RestartCommand.Subscribe(_ => RestartOnClick().Forget()).AddTo(Disposables);
        }
        
        private void HomeOnClick()
        {
        }
        
        private async UniTaskVoid RestartOnClick()
        {
            await _gameManager.RestartLevel();
            WindowsManager.HideWindow<FailWindowPresenter>();
        }
    }
}