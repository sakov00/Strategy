using System.Collections.Generic;
using _General.Scripts.AllAppData;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Concrete.Player
{
    public class PlayerController : UnitController
    {
        [Inject] private AppData _appData;
        [field: SerializeField] public PlayerModel Model { get; private set; }
        [field: SerializeField] public PlayerView View { get; private set; }
        protected override UnitModel UnitModel => Model;
        protected override UnitView UnitView => View;
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private PlayerMovementSystem _playerMovementSystem;
        private RegenerationHpSystem _regenerationHpSystem;

        public override void Initialize()
        {
            base.Initialize();
            
            Model.CurrentHealth = Model.MaxHealth;
            
            _playerMovementSystem = new PlayerMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHpSystem(Model, View);
            View.Initialize();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }

        private void Update()
        {
            _playerMovementSystem.MoveTo(_appData.LevelData.MoveDirection);
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is PlayerModel playerModel)
            {
                Model = playerModel;
                Initialize();
            }
        }
        
        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            _regenerationHpSystem?.Dispose();
            _damageSystem?.Dispose();
        }
    }
}