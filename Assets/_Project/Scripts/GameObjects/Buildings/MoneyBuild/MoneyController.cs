using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Windows.Presenters;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyController
    {
        private readonly MoneyBuildModel moneyBuildModel;
        private readonly MoneyBuildingView moneyBuildingView;
        private readonly GameWindowViewModel gameWindowViewModel;

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView, GameWindowViewModel gameWindowViewModel)
        {
            this.moneyBuildModel = moneyBuildModel;
            this.moneyBuildingView = moneyBuildingView;
            this.gameWindowViewModel = gameWindowViewModel;
            GameTimer.Instance.OnEverySecond += AddMoneyToPlayer;
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = moneyBuildModel.addMoneyValue * moneyBuildModel.CurrentLevel;
            gameWindowViewModel.AddMoney(moneyAmount);
            moneyBuildingView.MoneyUp(moneyAmount);
        }

        public void Dispose()
        {
            GameTimer.Instance.OnEverySecond -= AddMoneyToPlayer;
        }
    }
}