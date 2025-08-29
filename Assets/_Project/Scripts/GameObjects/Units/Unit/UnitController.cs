using System.Collections.Generic;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.Units.Character;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Units.Unit
{
    public abstract class UnitController : MyCharacterController
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [field:SerializeField] public UnitModel Model { get; set; }
        [field:SerializeField] public UnitView View { get; set; }
        public override CharacterModel CharacterModel => Model;
        public override CharacterView CharacterView => View;

        private UnitMovementSystem _unitMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHPSystem _regenerationHpSystem;

        protected override void Initialize()
        {
            base.Initialize();
            _objectsRegistry.Register(this);
            
            _unitMovementSystem = new UnitMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            
            if(Model.WarSide == WarSide.Friend)
                _regenerationHpSystem = new RegenerationHPSystem(Model, View);
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
            Model.WayToAim = positions;
        }
        
        public override void ClearData()
        {
            _regenerationHpSystem.Dispose();
            Destroy(gameObject);
        }
    }
}