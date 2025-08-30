using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    public class MoneyBuildController : BuildController
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
        
        public override ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(MoneyBuildController),
                position = transform.position,
                rotation = transform.rotation
            };
            return objectJson;
        }

        public override void SetJsonData(ObjectJson objectJson)
        {
            transform.position = objectJson.position;
            transform.rotation = objectJson.rotation;
        }

        private void OnDestroy()
        {
            _moneyController.Dispose();
        }
        
        public override void ClearData()
        {
            Destroy(gameObject);
        }
    }
}