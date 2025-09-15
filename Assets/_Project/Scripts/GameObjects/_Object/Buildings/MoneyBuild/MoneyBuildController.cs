using _General.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [field:SerializeField] public MoneyBuildModel Model { get; private set; }
        [field: SerializeField] public MoneyBuildingView View { get; private set; }
        public override BuildModel BuildModel => Model;
        public override BuildView BuildView => View;

        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
            AppData.LevelEvents.WinEvent += AddMoneyToPlayer;
        }
        
        public override void BuildInGame()
        {

        }
        
        private void AddMoneyToPlayer()
        {
            var moneyAmount = Model.AddMoneyValue * (Model.CurrentLevel + 1);
            AppData.LevelData.Money += moneyAmount;
            View.MoneyUp(moneyAmount);
        }
        
        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is MoneyBuildModel moneyBuildModel)
            {
                Model = moneyBuildModel;
                Initialize();
            }
        }
        
        public override void Restore()
        {
            base.Restore();
            ObjectsRegistry.Register(this);
        }

        public override void ReturnToPool()
        {
            ClearData();
            BuildPool.Return(this);
        }

        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
            AppData.LevelEvents.WinEvent -= AddMoneyToPlayer;
        }
    }
}