using System.Collections.Generic;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using Cysharp.Threading.Tasks;
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
        private UnitMovementSystem _unitMovementSystem;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _unitMovementSystem?.MoveToAim();
            _damageSystem?.Attack();
        }

        public override UniTask InitializeAsync()
        {
            base.InitializeAsync();
            
            Model.CurrentHealth = Model.MaxHealth;

            _unitMovementSystem = new UnitMovementSystem(Model, View, transform);
            _damageSystem = new DamageSystem(this, transform);
            View.Initialize();
            return default;
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel) =>
            Model.LoadData(savableModel);
        
        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            _damageSystem?.Dispose();
        }
    }
}