using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.ArcherEnemy
{
    public class ArcherEnemyController : UnitController
    {
        [field: SerializeField] public ArcherEnemyModel Model { get; private set; }
        [field: SerializeField] public ArcherEnemyView View { get; private set; }
        protected override UnitModel UnitModel => Model;
        protected override UnitView UnitView => View;
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private UnitMovementSystem _unitMovementSystem;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim?.DetectAim();
            _unitMovementSystem?.MoveToAim();
            _damageSystem?.Attack();
        }

        public override void Initialize()
        {
            base.Initialize();
            
            Model.CurrentHealth = Model.MaxHealth;
            
            _unitMovementSystem = new UnitMovementSystem(Model, View, transform);
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
            if (savableModel is ArcherEnemyModel unitModel)
            {
                Model = unitModel;
            }
        }

        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            _damageSystem?.Dispose();
        }
    }
}