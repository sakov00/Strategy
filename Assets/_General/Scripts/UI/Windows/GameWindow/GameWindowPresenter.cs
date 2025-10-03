using System;
using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows.BaseWindow;
using _General.Scripts.UI.Windows.FailWindow;
using _General.Scripts.UI.Windows.PauseWindow;
using _General.Scripts.UI.Windows.WinWindow;
using _Project.Scripts;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
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
        [Inject] private GameManager _gameManager;
        
        private Action _winEventHandler;
        private Action _failEventHandler;
        
        [SerializeField] private GameWindowModel _model;
        [SerializeField] private GameWindowView _view;
        
        public override BaseWindowModel Model => _model;
        public override BaseWindowView View => _view;
        
        public ReactiveCommand OpenSettingsWindowCommand { get; } = new();
        public ReactiveCommand OpenPauseWindowCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReactiveProperty<bool> IsStrategyMode => _model.IsStrategyModeReactive;

        public override void Initialize()
        {
            base.Initialize();
            OpenSettingsWindowCommand.Subscribe(_ => OpenSettingsWindow()).AddTo(Disposables);
            OpenPauseWindowCommand.Subscribe(_ => OpenPauseWindow()).AddTo(Disposables);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(Disposables);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(Disposables);
            
            _winEventHandler = () => UniTask.Void(WinHandle);
            _failEventHandler = () => UniTask.Void(FailHandle);
            _appData.LevelEvents.WinEvent += _winEventHandler;
            _appData.LevelEvents.FailEvent += _failEventHandler;
        }

        private void OpenPauseWindow() => WindowsManager.ShowWindow<PauseWindowPresenter>();
        private void OpenSettingsWindow() => WindowsManager.ShowWindow<PauseWindowPresenter>();
        private void SetStrategyMode() => _model.IsStrategyMode = !_model.IsStrategyMode;
        private void NextRoundOnClick()
        {
            _appData.LevelData.IsFighting = true;
            
            _appData.LevelData.ObjectsForRestoring.Clear();
            foreach (var objectController in _objectsRegistry.GetAllByInterface<ObjectController>())
                _appData.LevelData.ObjectsForRestoring.Add(objectController.GetSavableModel());
            
            foreach (var spawnPoint in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawnPoint.StartSpawn();
            _appData.LevelEvents.Initialize();
        }

        private async UniTaskVoid WinHandle()
        {
            _appData.LevelEvents.Dispose();
            _appData.LevelData.CurrentRound++;
            _appData.LevelData.IsFighting = false;
            
            var spawns = _objectsRegistry.GetTypedList<EnemyRoadController>();
            var isLastRound = spawns.Any(spawn => spawn.CountRounds == _appData.LevelData.CurrentRound);
            if (isLastRound) _appData.User.CurrentLevel++;
            
            var winWindow = WindowsManager.GetWindow<WinWindowPresenter>();
            winWindow.SetWindowData(isLastRound);
            await WindowsManager.ShowWindow<WinWindowPresenter>();
            await _gameManager.ResetRound();
        }

        private async UniTaskVoid FailHandle()
        {
            _appData.LevelEvents.Dispose();
            await WindowsManager.ShowWindow<FailWindowPresenter>();
        }

        public override void Dispose()
        {
            base.Dispose();
            _model.IsStrategyMode = false;
            _appData.LevelEvents.WinEvent -= _winEventHandler;
            _appData.LevelEvents.FailEvent -= _failEventHandler;
        }
    }
}