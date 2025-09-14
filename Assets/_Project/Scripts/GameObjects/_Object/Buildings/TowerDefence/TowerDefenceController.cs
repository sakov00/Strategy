using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.TowerDefence
{
    public class TowerDefenceController : BuildController
    {
        [field:SerializeField] public TowerDefenceModel Model { get; private set; }
        [field:SerializeField] public TowerDefenceView View { get; private set; }
        public override BuildModel BuildModel => Model;
        public override BuildView BuildView => View;
        
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;

        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
        }
        
        public override ISavableModel GetSavableModel()
        {
            Model.Position = transform.position;
            Model.Rotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is TowerDefenceModel towerDefenceModel)
            {
                Model = towerDefenceModel;
                Initialize();
            }
        }

        protected override void FixedUpdate()
        {            
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public override void Restore()
        {
            base.Restore();
            ObjectsRegistry.Register(this);
        }
        
        public override void ReturnToPool()
        {
            BuildPool.Return(this);
        }
        
        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
        }
    }
}