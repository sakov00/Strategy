using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.Characters.Unit
{
    public abstract class UnitController : MyCharacterController
    {
        [field:SerializeField] public UnitModel Model { get; protected set; }
        [field:SerializeField] public UnitView View  { get; protected set; }
        public override CharacterModel CharacterModel => Model;
        public override CharacterView CharacterView => View;

        private UnitMovementSystem _unitMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHpSystem _regenerationHpSystem;

        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
            _unitMovementSystem = new UnitMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);

            if (Model.WarSide == WarSide.Friend)
            {
                Model.WayToAim = new List<Vector3> {transform.position};
                _regenerationHpSystem = new RegenerationHpSystem(Model, View);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _unitMovementSystem.MoveToAim();
            _damageSystem.Attack();
        }
        
        public override void ReturnToPool()
        {
            CharacterPool.Return(this);
            if(Model.WarSide == WarSide.Enemy)
                ObjectsRegistry.Unregister(this);
        }
        
        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
            _regenerationHpSystem?.Dispose();
        }
    }
}