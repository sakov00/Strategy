using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows.PauseWindow;
using _Project.Scripts;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects.EnemyRoads;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.GameWindow
{
    public class GameWindowViewModel : BaseWindowViewModel
    {
        [Inject] private AppData _appData;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] private GameWindowModel _model;
        
        protected override BaseWindowModel BaseModel => _model;
        
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
            _appData.LevelEvents.AllEnemiesKilled += NextRoundAvailable;
        }

        private void OpenPauseWindow()
        {
            WindowsManager.ShowWindow<PauseWindowView>();
        }
        
        private void SetStrategyMode() => _model.IsStrategyMode = !_model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            _model.IsNextRoundAvailable = false;
            foreach (var spawnPoint in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawnPoint.StartSpawn();
        }

        private void NextRoundAvailable()
        {
            _appData.LevelData.CurrentRound++;
            _model.IsNextRoundAvailable = true;
            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawn.RefreshInfoRound();
        }

        public void Reset()
        {
            _model.IsStrategyMode = false;
            _model.IsNextRoundAvailable = true;
        }
    }
}