using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.TowerDefence
{
    public class TowerDefenceController : BuildController
    {
        [field: SerializeField] public TowerDefenceModel Model { get; private set; }
        [field: SerializeField] public TowerDefenceView View { get; private set; }
        protected override BuildModel BuildModel => Model;
        protected override BuildView BuildView => View;
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }

        public override void Initialize()
        {
            base.Initialize();
            
            Model.CurrentHealth = Model.MaxHealth;

            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            View.Initialize();
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
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
        
        public override void DeleteFromScene(bool realDelete = false)
        {
            base.DeleteFromScene(realDelete);
            _damageSystem?.Dispose();
        }
    }
}