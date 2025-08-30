using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    public class TowerDefenceController : BuildController
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] protected TowerDefenceModel _model;
        [SerializeField] protected TowerDefenceView _view;
        public override BuildModel BuildModel => _model;
        public override BuildView BuildView => _view;
        
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;

        protected override void Initialize()
        {
            base.Initialize();
            _objectsRegistry.Register(this);
            _detectionAim = new DetectionAim(_model, transform);
            _damageSystem = new DamageSystem(_model, _view, transform);
        }
        
        public override ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(TowerDefenceController),
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

        protected override void FixedUpdate()
        {            
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public override void ClearData()
        {
            Destroy(gameObject);
        }
    }
}