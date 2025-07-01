using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    public class TowerDefenceController : BuildController, ISavable<TowerDefenceBuildJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        
        [SerializeField] protected TowerDefenceModel model;
        [SerializeField] protected TowerDefenceView view;
        public override BuildModel BuildModel => model;
        public override BuildView BuildView => view;
        
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        protected override void Initialize()
        {
            _saveRegistry.Register(this);
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);
            base.Initialize();
        }
        
        public TowerDefenceBuildJson GetJsonData()
        {
            var towerDefenceBuildJson = new TowerDefenceBuildJson();
            towerDefenceBuildJson.position = transform.position;
            towerDefenceBuildJson.rotation = transform.rotation;
            towerDefenceBuildJson.towerDefenceModel = model;
            return towerDefenceBuildJson;
        }

        public void SetJsonData(TowerDefenceBuildJson towerDefenceBuildJson)
        {
            transform.position = towerDefenceBuildJson.position;
            transform.rotation = towerDefenceBuildJson.rotation;
            model = towerDefenceBuildJson.towerDefenceModel;
        }

        protected override void FixedUpdate()
        {            
            base.FixedUpdate();
            detectionAim.DetectAim();
            damageSystem.Attack();
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}