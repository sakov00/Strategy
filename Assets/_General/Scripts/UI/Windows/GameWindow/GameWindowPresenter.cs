using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows.FailWindow;
using _General.Scripts.UI.Windows.PauseWindow;
using _General.Scripts.UI.Windows.WinWindow;
using _Project.Scripts.GameObjects.EnemyRoads;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.GameWindow
{
    public class GameWindowPresenter : BaseWindowPresenter
    {
        [Inject] private AppData _appData;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private ResetLevelService _resetLevelService;
        
        [SerializeField] private GameWindowModel _model;
        [SerializeField] private GameWindowView _view;
        
        protected override BaseWindowModel BaseModel => _model;
        protected override BaseWindowView BaseView => _view;

        public ReactiveCommand OpenPauseWindowCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReactiveProperty<bool> IsStrategyMode => _model.IsStrategyModeReactive;
        public IReactiveProperty<bool> IsNextRoundAvailable => _model.IsNextRoundAvailableReactive;

        protected override void Awake()
        {
            base.Awake();
            OpenPauseWindowCommand.Subscribe(_ => OpenPauseWindow()).AddTo(this);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(this);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(this);
        }

        public void Initialize()
        {
            _appData.LevelEvents.WinEvent += WinHandle;
            _appData.LevelEvents.FailEvent += FailHandle;
        }

        private void OpenPauseWindow()
        {
            WindowsManager.ShowWindow<PauseWindowPresenter>();
        }
        
        private void SetStrategyMode() => _model.IsStrategyMode = !_model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            _model.IsNextRoundAvailable = false;
            foreach (var spawnPoint in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawnPoint.StartSpawn();
        }

        private async void WinHandle()
        {
            _appData.User.CurrentRound++;
            _model.IsNextRoundAvailable = true;
            
            var spawns = _objectsRegistry.GetTypedList<EnemyRoadController>();
            var isLastRound = spawns.Any(spawn => spawn.CountRounds == _appData.User.CurrentRound);
            
            var winWindow = WindowsManager.GetWindowOrInstantiate<WinWindowPresenter>();
            winWindow.Initialize(isLastRound);
            await WindowsManager.ShowWindow<WinWindowPresenter>();
            
            if (isLastRound)
            {
                _appData.User.CurrentLevel++;
            }
            else
            {
                _resetLevelService.ResetRound();
                foreach (var spawn in spawns)
                    spawn.RefreshInfoRound();
            }
        }

        private void FailHandle()
        {
            WindowsManager.ShowWindow<FailWindowPresenter>();
        }

        public void Reset()
        {
            _model.IsStrategyMode = false;
            _model.IsNextRoundAvailable = true;
        }

        public void Dispose()
        {
            _appData.LevelEvents.WinEvent -= WinHandle;
            _appData.LevelEvents.FailEvent -= FailHandle;
        }
    }
}