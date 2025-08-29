using System.Linq;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows.FailWindow;
using _General.Scripts.UI.Windows.PauseWindow;
using _General.Scripts.UI.Windows.WinWindow;
using _Project.Scripts;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.Units.Unit;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.GameWindow
{
    public class GameWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private GameWindowModel _model;
        
        [Inject] private GameManager _gameManager;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        protected override BaseWindowModel BaseModel => _model;
        
        public ReactiveCommand OpenPauseWindowCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
#if EDIT_MODE
        public ReactiveCommand FastFailCommand { get; } = new();
        public ReactiveCommand FastWinCommand { get; } = new();
        public ReactiveCommand<int> SaveLevelCommand { get; } = new();
        public ReactiveCommand<int> LoadLevelCommand { get; } = new();
#endif
        
        public IReactiveProperty<bool> IsStrategyMode => _model.IsStrategyModeReactive;
        public IReactiveProperty<bool> IsNextRoundAvailable => _model.IsNextRoundAvailableReactive;

        protected override void Awake()
        {
            base.Awake();
            OpenPauseWindowCommand.Subscribe(_ => OpenPauseWindow()).AddTo(this);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(this);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(this);
            
            _objectsRegistry
                .GetTypedList<UnitController>()
                .ObserveRemove()
                .Subscribe(_ => AllEnemiesDestroyed());
#if EDIT_MODE
            FastFailCommand.Subscribe(_ => OpenFailWindow());
            FastWinCommand.Subscribe(_ => OpenWinWindow());
            SaveLevelCommand.Subscribe(levelIndex => _gameManager.SaveLevel(levelIndex)).AddTo(this);
            LoadLevelCommand.Subscribe(levelIndex => _gameManager.StartLevel(levelIndex)).AddTo(this);
#endif
        }
        
        private void OpenFailWindow()
        {
            WindowsManager.ShowWindow<FailWindowView>();
        }
        
        private void OpenWinWindow()
        {
            WindowsManager.ShowWindow<WinWindowView>();
        }

        private void OpenPauseWindow()
        {
            WindowsManager.ShowWindow<PauseWindowView>();
        }
        
        private void SetStrategyMode() => _model.IsStrategyMode = !_model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            _model.IsNextRoundAvailable = false;
            _gameManager.NextRound();
        }

        private void AllEnemiesDestroyed()
        {
            if (_objectsRegistry.GetTypedList<UnitController>().Any(x => x.ObjectModel.WarSide == WarSide.Enemy)) 
                return;

            _model.IsNextRoundAvailable = true;
        }

        public void Reset()
        {
            _model.IsStrategyMode = false;
            _model.IsNextRoundAvailable = true;
        }
    }
}