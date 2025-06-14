using _Project.Scripts._GlobalLogic;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyController
    {
        private readonly MoneyBuildModel moneyBuildModel;
        private readonly MoneyBuildingView moneyBuildingView;

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView)
        {
            this.moneyBuildModel = moneyBuildModel;
            this.moneyBuildingView = moneyBuildingView;
            GameTimer.Instance.OnEverySecond += AddMoneyToPlayer;
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = moneyBuildModel.addMoneyValue * moneyBuildModel.CurrentLevel;
            GlobalObjects.GameData.levelData.Money += moneyAmount;
            moneyBuildingView.MoneyUp(moneyAmount);
        }

        public void Dispose()
        {
            GameTimer.Instance.OnEverySecond -= AddMoneyToPlayer;
        }
    }
}