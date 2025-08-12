using System;
using System.Linq;
using _Project.Scripts._GlobalData;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Registries;
using UniRx;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyController
    {
        [Inject] private HealthRegistry _healthRegistry;
        
        private readonly MoneyBuildModel _moneyBuildModel;
        private readonly MoneyBuildingView _moneyBuildingView;
        
        private readonly CompositeDisposable _disposables = new();

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView)
        {
            _moneyBuildModel = moneyBuildModel;
            _moneyBuildingView = moneyBuildingView;
            
            InjectManager.Inject(this);
            _healthRegistry
                .GetAll()
                .ObserveRemove()
                .Skip(1)
                .Subscribe(_ => AllEnemiesDestroyed())
                .AddTo(_disposables);
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_healthRegistry.HasEnemies()) 
                return;

            AddMoneyToPlayer();
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = _moneyBuildModel.AddMoneyValue * (_moneyBuildModel.CurrentLevel + 1);
            AppData.User.Money += moneyAmount;
            _moneyBuildingView.MoneyUp(moneyAmount);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}