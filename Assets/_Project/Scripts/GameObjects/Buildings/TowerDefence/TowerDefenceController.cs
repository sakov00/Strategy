using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    public class TowerDefenceController : BuildController, ISavable<TowerDefenceBuildJson>
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
        
        public TowerDefenceBuildJson GetJsonData()
        {
            var towerDefenceBuildJson = new TowerDefenceBuildJson();
            towerDefenceBuildJson.position = transform.position;
            towerDefenceBuildJson.rotation = transform.rotation;
            towerDefenceBuildJson.towerDefenceModel = _model;
            return towerDefenceBuildJson;
        }

        public void SetJsonData(TowerDefenceBuildJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _model = environmentJson.towerDefenceModel;
        }

        protected override void FixedUpdate()
        {            
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}