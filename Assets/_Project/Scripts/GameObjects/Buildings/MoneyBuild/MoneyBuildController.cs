using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController, ISavable<MoneyBuildJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        
        [SerializeField] protected MoneyBuildModel model;
        [SerializeField] protected MoneyBuildingView view;
        public override BuildModel BuildModel => model;
        public override BuildView BuildView => view;
        
        private MoneyController moneyController;

        protected override void Initialize()
        {
            _saveRegistry.Register(this);
            moneyController = new MoneyController(model, view);
            base.Initialize();
        }
        
        public MoneyBuildJson GetJsonData()
        {
            var moneyBuildJson = new MoneyBuildJson();
            moneyBuildJson.position = transform.position;
            moneyBuildJson.rotation = transform.rotation;
            moneyBuildJson.moneyBuildModel = model;
            return moneyBuildJson;
        }

        public void SetJsonData(MoneyBuildJson moneyBuildJson)
        {
            transform.position = moneyBuildJson.position;
            transform.rotation = moneyBuildJson.rotation;
            model = moneyBuildJson.moneyBuildModel;
        }

        private void OnDestroy()
        {
            moneyController.Dispose();
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}