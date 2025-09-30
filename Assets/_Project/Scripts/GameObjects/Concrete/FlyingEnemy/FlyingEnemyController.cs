using System.Collections.Generic;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.FlyingEnemy
{
    public class FlyingEnemyController : UnitController
    {
        [field: SerializeField] public FlyingEnemyModel Model { get; private set; }
        [field: SerializeField] public FlyingEnemyView View { get; private set; }
        
        protected override UnitModel UnitModel => Model;
        protected override UnitView UnitView => View;
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private UnitMovementSystem _unitMovementSystem;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _unitMovementSystem.MoveToAim();
            _damageSystem.Attack();
        }

        public override void Initialize()
        {
            InjectManager.Inject(this);
            ObjectsRegistry.Register(this);
            
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
            if (savableModel is FlyingEnemyModel unitModel)
            {
                Model = unitModel;
                Initialize();
            }
        }

        public override void ReturnToPool()
        {
            UnitPool.Return(this);
            ObjectsRegistry.Unregister(this);
            OnKilled?.Invoke(this);
        }

        public override void ClearData()
        {
            OnKilled = null;
            ObjectsRegistry.Unregister(this);
            _damageSystem?.Dispose();
        }
    }
}