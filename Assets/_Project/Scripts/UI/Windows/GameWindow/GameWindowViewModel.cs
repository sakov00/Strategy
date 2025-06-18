using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowViewModel : BaseWindowPresenter
    {
        [Inject] private ResetService resetService;
        [Inject] private HealthRegistry healthRegistry;
        [Inject] private SpawnRegistry spawnRegistry;

        [SerializeField] private GameWindowModel model;
        
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReadOnlyReactiveProperty<bool> IsNextRoundAvailable => model.IsNextRoundAvailableReactive;
        public IReadOnlyReactiveProperty<bool> IsStrategyMode => model.IsStrategyModeReactive;
        public IReadOnlyReactiveProperty<int> CurrentRound => model.CurrentRoundReactive;
        public IReadOnlyReactiveProperty<int> Money => model.MoneyReactive;
        
        public Vector3 MoveDirection { get; set; }
        
        public int GetCurrentRound() => model.CurrentRound;
        public void AddMoney(int amount) => model.Money += amount;

        private void Awake()
        {
            healthRegistry
                .GetAll()
                .ObserveRemove()
                .Subscribe(_ => TryStartNewRound())
                .AddTo(this);
            
            SetStrategyModeCommand
                .Subscribe(_ => SetStrategyMode())
                .AddTo(this);
            NextRoundCommand
                .Subscribe(_ => NextRoundOnClick())
                .AddTo(this);
        }
        
        private void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            model.IsNextRoundAvailable = false;
            foreach (var spawnPoint in spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
        
        private void TryStartNewRound()
        {
            bool enemiesRemain = healthRegistry.GetAll().Any(unit => unit.WarSide == WarSide.Enemy);
            if (enemiesRemain) return;

            resetService.ResetRound();
            model.CurrentRound++;
            model.IsNextRoundAvailable = true;
        }
    }
}