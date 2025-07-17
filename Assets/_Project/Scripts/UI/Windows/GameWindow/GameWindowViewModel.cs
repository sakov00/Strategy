using System;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Registries;
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
        [SerializeField] private GameWindowModel model;
        
        [Inject] private GameManager _gameManager;
        [Inject] private HealthRegistry _healthRegistry;
        
        public ReactiveCommand OpenPauseWindowCommand { get; } = new();
        public ReactiveCommand<int> SaveLevelCommand { get; } = new();
        public ReactiveCommand<int> LoadLevelCommand { get; } = new();
        public ReactiveCommand NextRoundCommand { get; } = new();
        public ReactiveCommand SetStrategyModeCommand { get; } = new();
        
        public IReactiveProperty<bool> IsStrategyMode => model.IsStrategyModeReactive;
        public IReactiveProperty<bool> IsNextRoundAvailable => model.IsNextRoundAvailableReactive;

        private void Awake()
        {
            OpenPauseWindowCommand.Subscribe(_ => OpenPauseWindow()).AddTo(this);
            SetStrategyModeCommand.Subscribe(_ => SetStrategyMode()).AddTo(this);
            NextRoundCommand.Subscribe(_ => NextRoundOnClick()).AddTo(this);
            
            _healthRegistry
                .GetAll()
                .ObserveRemove()
                .Subscribe(_ => AllEnemiesDestroyed());
#if EDIT_MODE
            SaveLevelCommand.Subscribe(levelIndex => _gameManager.SaveLevel(levelIndex)).AddTo(this);
            LoadLevelCommand.Subscribe(levelIndex => _gameManager.StartLevel(levelIndex)).AddTo(this);
#endif
        }

        private void OpenPauseWindow()
        {
            WindowsManager.ShowWindow<PauseWindowView>();
        }
        
        private void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        
        private void NextRoundOnClick()
        {
            model.IsNextRoundAvailable = false;
            _gameManager.NextRound();
        }

        private void AllEnemiesDestroyed()
        {
            if (_healthRegistry.HasEnemies()) 
                return;

            model.IsNextRoundAvailable = true;
        }

        public void Reset()
        {
            model.IsStrategyMode = false;
            model.IsNextRoundAvailable = true;
        }
    }
}