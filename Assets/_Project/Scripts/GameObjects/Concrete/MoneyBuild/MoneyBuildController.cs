using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [field: SerializeField] public MoneyBuildModel Model { get; private set; }
        [field: SerializeField] public MoneyBuildingView View { get; private set; }
        protected override BuildModel BuildModel => Model;
        protected override BuildView BuildView => View;
        
        public override void Initialize()
        {
            InjectManager.Inject(this);
            ObjectsRegistry.Register(this);
            
            Model.CurrentHealth = Model.MaxHealth;
            
            View.Initialize();
            AppData.LevelEvents.WinEvent += AddMoneyToPlayer;
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = Model.AddMoneyValue;
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