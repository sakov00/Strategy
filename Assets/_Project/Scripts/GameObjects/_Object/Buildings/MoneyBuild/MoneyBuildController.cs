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

        private MoneyController _moneyController;

        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
            _moneyController = new MoneyController(Model, View);
        }
        
        public override ISavableModel GetSavableModel()
        {
            Model.Position = transform.position;
            Model.Rotation = transform.rotation;
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
        
        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
            _moneyController.Dispose();
        }
    }
}