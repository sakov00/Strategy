using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController, ISavable<MoneyBuildJson>
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] protected MoneyBuildModel _model;
        [SerializeField] protected MoneyBuildingView _view;
        public override BuildModel BuildModel => _model;
        public override BuildView BuildView => _view;
        
        private MoneyController _moneyController;

        protected override void Initialize()
        {
            base.Initialize();
            _objectsRegistry.Register(this);
            _moneyController = new MoneyController(_model, _view);
        }
        
        public MoneyBuildJson GetJsonData()
        {
            var moneyBuildJson = new MoneyBuildJson();
            moneyBuildJson.position = transform.position;
            moneyBuildJson.rotation = transform.rotation;
            moneyBuildJson.moneyBuildModel = _model;
            return moneyBuildJson;
        }

        public void SetJsonData(MoneyBuildJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _model = environmentJson.moneyBuildModel;
        }

        private void OnDestroy()
        {
            _moneyController.Dispose();
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}