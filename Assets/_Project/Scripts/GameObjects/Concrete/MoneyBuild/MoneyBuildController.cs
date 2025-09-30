using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    public class MoneyBuildController : BuildController
    {
        [field: SerializeField] public MoneyBuildModel Model { get; private set; }
        [field: SerializeField] public MoneyBuildingView View { get; private set; }
        
        public override WarSide WarSide => Model.WarSide;
        public override float CurrentHealth { get => Model.CurrentHealth; set => Model.CurrentHealth = value; }

        protected void FixedUpdate()
        {
            View.UpdateHealthBar(Model.CurrentHealth, Model.MaxHealth);
        }
        
        public override void Initialize()
        {
            InjectManager.Inject(this);

            View.Initialize();
            HeightObject = View.GetHeightObject();
            Model.NoAimPos = transform.position;
            ObjectsRegistry.Register(this);
            ObjectsRegistry.Register(this);
            AppData.LevelEvents.WinEvent += AddMoneyToPlayer;
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
            transform.SetParent(null);
            Model.CurrentHealth = Model.MaxHealth;
            Model.NoAimPos = transform.position;
            BuildPool.Remove(this);
            gameObject.SetActive(true);
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