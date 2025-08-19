using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    public class UnitController : MyCharacterController, ISavable<UnitJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private ClearDataRegistry _clearDataRegistry;
        
        [SerializeField] private UnitModel _model;
        [SerializeField] private UnitView _view;
        protected override CharacterModel CharacterModel => _model;
        protected override CharacterView CharacterView => _view;

        private UnitMovementSystem _unitMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHPSystem _regenerationHpSystem;

        protected override void Initialize()
        {
            base.Initialize();
            _saveRegistry.Register(this);
            _clearDataRegistry.Register(this);
            
            _unitMovementSystem = new UnitMovementSystem(_model, _view, transform);
            _detectionAim = new DetectionAim(_model, transform);
            _damageSystem = new DamageSystem(_model, _view, transform);
            
            if(_model.WarSide == WarSide.Friend)
                _regenerationHpSystem = new RegenerationHPSystem(_model, _view);
        }
        
        public UnitJson GetJsonData()
        {
            var unitJson = new UnitJson
            {
                position = transform.position,
                rotation = transform.rotation,
                unitModel = _model
            };
            return unitJson;
        }

        public void SetJsonData(UnitJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _model = environmentJson.unitModel;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _unitMovementSystem.MoveToAim();
            _damageSystem.Attack();
        }

        public void SetNoAimPosition(List<Vector3> positions)
        {
            _model.WayToAim = positions;
        }
        
        public void ClearData()
        {
            _regenerationHpSystem.Dispose();
            Destroy(gameObject);
        }
    }
}