using System;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Registries;
using _Project.Scripts.Windows.Presenters;
using UniRx;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyController
    {
        [Inject] private HealthRegistry _healthRegistry;
        
        private readonly MoneyBuildModel moneyBuildModel;
        private readonly MoneyBuildingView moneyBuildingView;
        
        private readonly CompositeDisposable disposables = new();

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView)
        {
            this.moneyBuildModel = moneyBuildModel;
            this.moneyBuildingView = moneyBuildingView;
            
            InjectManager.Inject(this);
            _healthRegistry
                .GetAll()
                .ObserveRemove()
                .Skip(1)
                .Subscribe(_ => AllEnemiesDestroyed())
                .AddTo(disposables);
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_healthRegistry.HasEnemies()) 
                return;

            AddMoneyToPlayer();
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = moneyBuildModel.addMoneyValue * (moneyBuildModel.CurrentLevel + 1);
            AppData.User.Money += moneyAmount;
            moneyBuildingView.MoneyUp(moneyAmount);
        }

        public void Dispose()
        {
            disposables?.Dispose();
        }
    }
}