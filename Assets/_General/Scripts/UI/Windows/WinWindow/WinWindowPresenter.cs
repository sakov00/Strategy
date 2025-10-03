using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows.BaseWindow;
using _Project.Scripts;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
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
        [Inject] private ObjectsRegistry _objectsRegistry;
        
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
            
            var spawns = _objectsRegistry.GetTypedList<EnemyRoadController>();
            _isLevelWin = spawns.Any(spawn => spawn.CountRounds == _appData.LevelData.CurrentRound);
            if (_isLevelWin) _appData.User.CurrentLevel++;
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