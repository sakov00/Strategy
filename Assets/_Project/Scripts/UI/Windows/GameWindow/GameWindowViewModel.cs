using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.PauseWindow;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowViewModel : BaseWindowViewModel
    {
        [Inject] private LevelController _levelController;
        [SerializeField] private GameWindowModel model;
        
        public ReactiveCommand OpenPauseWindowCommand { get; } = new();
        public ReactiveCommand<int> SaveLevelCommand { get; } = new();
        public ReactiveCommand<int> LoadLevelCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReactiveProperty<bool> IsStrategyMode => model.IsStrategyModeReactive;

        private void Awake()
        {
            OpenPauseWindowCommand.Subscribe(_ => OpenPauseWindow()).AddTo(this);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(this);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(this);
#if EDIT_MODE
            SaveLevelCommand.Subscribe(levelIndex => _levelController.SaveLevel(levelIndex)).AddTo(this);
            LoadLevelCommand.Subscribe(levelIndex => _levelController.LoadLevel(levelIndex)).AddTo(this);
#endif
        }

        private void OpenPauseWindow()
        {
            WindowsManager.ShowWindow<PauseWindowView>();
        }
        
        private void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            AppData.LevelData.IsNextRoundAvailable = false;
            _levelController.NextRound();
        }
    }
}