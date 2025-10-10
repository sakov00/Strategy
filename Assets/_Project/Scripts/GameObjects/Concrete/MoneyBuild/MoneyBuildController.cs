using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [field: SerializeField] public MoneyBuildModel Model { get; private set; }
        [field: SerializeField] public MoneyBuildingView View { get; private set; }
        protected override BuildModel BuildModel => Model;
        protected override BuildView BuildView => View;
        
        public override UniTask InitializeAsync()
        {
            base.InitializeAsync();
            
            Model.CurrentHealth = Model.MaxHealth;
            
            View.Initialize();
            AppData.LevelEvents.WinEvent += AddMoneyToPlayer;
            return default;
        }

        private UniTaskVoid AddMoneyToPlayer()
        {
            var moneyAmount = Model.AddMoneyValue;
            AppData.LevelData.LevelMoney += moneyAmount;
            View.MoneyUp(moneyAmount);
            return default;
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
            }
        }
        
        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            AppData.LevelEvents.WinEvent -= AddMoneyToPlayer;
        }
    }
}