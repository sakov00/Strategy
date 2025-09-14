using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.MoneyBuild;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.MainBuild
{
    public class MainBuildController : BuildController
    {
        [field:SerializeField] public MainBuildModel Model { get; private set; }
        [field: SerializeField] public MainBuildingView View { get; private set; }
        public override BuildModel BuildModel => Model;
        public override BuildView BuildView => View;

        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
        }
        
        public override ISavableModel GetSavableModel()
        {
            Model.Position = transform.position;
            Model.Rotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is MainBuildModel moneyBuildModel)
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
        }
    }
}