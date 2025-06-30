using _Project.Scripts.Services;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowViewModel : BaseWindowPresenter
    {
        [Inject] private LevelController _levelController;
        [SerializeField] private GameWindowModel model;
        
        public ReactiveCommand SaveLevelCommand { get; } = new();
        public ReactiveCommand<int> LoadLevelCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReactiveProperty<bool> IsNextRoundAvailable => model.IsNextRoundAvailableReactive;
        public IReactiveProperty<bool> IsStrategyMode => model.IsStrategyModeReactive;
        public IReactiveProperty<int> CurrentRound => model.CurrentRoundReactive;
        public IReactiveProperty<int> Money => model.MoneyReactive;
        
        public Vector3 MoveDirection { get; set; }

        private void Awake()
        {
            _levelController.RoundUpdated.Subscribe(_ => NewRoundEvent()).AddTo(this);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(this);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(this);
#if EDIT_MODE
            SaveLevelCommand.Subscribe(_ => _levelController.SaveLevel()).AddTo(this);
            LoadLevelCommand.Subscribe(levelIndex => _levelController.LoadLevel(levelIndex)).AddTo(this);
#endif
        }

        private void NewRoundEvent()
        {
            CurrentRound.Value++;
            IsNextRoundAvailable.Value = true;
        }
        
        private void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            IsNextRoundAvailable.Value = false;
            _levelController.NextRound();
        }
    }
}