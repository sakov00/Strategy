using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowViewModel : BaseWindowPresenter
    {
        [Inject] private ResetService _resetService;
        
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private BuildingZoneRegistry _buildingZoneRegistry;
        [Inject] private JsonLoader<LevelJson> _jsonLoader;

        [SerializeField] private GameWindowModel model;
        
        public ReactiveCommand SaveLevelCommand { get; } = new();
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
            _healthRegistry
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
            
#if EDIT_MODE
            SaveLevelCommand
                .Subscribe(_ => SaveLevel())
                .AddTo(this);
#endif
        }
        
        private void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            model.IsNextRoundAvailable = false;
            foreach (var spawnPoint in _spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
        
        private void TryStartNewRound()
        {
            bool enemiesRemain = _healthRegistry.GetAll().Any(unit => unit.WarSide == WarSide.Enemy);
            if (enemiesRemain) return;

            _resetService.ResetRound();
            model.CurrentRound++;
            model.IsNextRoundAvailable = true;
        }

        private void SaveLevel()
        {
            var levelJson = new LevelJson
            {
                spawnPoints = _spawnRegistry.GetAll().ToList(),
                buildingZoneModels = _buildingZoneRegistry.GetAll().ToList()
            };
            _jsonLoader.Save(levelJson);
        }
    }
}