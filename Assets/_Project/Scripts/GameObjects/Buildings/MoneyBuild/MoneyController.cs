using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Windows.Presenters;
using UniRx;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyController
    {
        private readonly MoneyBuildModel moneyBuildModel;
        private readonly MoneyBuildingView moneyBuildingView;
        
        private readonly CompositeDisposable disposables = new();

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView)
        {
            this.moneyBuildModel = moneyBuildModel;
            this.moneyBuildingView = moneyBuildingView;
            
            InjectManager.Inject(this);
            AppData.LevelData.IsNextRoundAvailableReactive
                .Skip(1)
                .Subscribe(isNextRound =>
                {
                    if (isNextRound) AddMoneyToPlayer();
                })
                .AddTo(disposables);
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