using System.Collections.Generic;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.WarriorFriend
{
    public class WarriorFriendController : UnitController
    {
        [field: SerializeField] public WarriorFriendModel Model { get; private set; }
        [field: SerializeField] public WarriorFriendView View { get; private set; }
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private RegenerationHpSystem _regenerationHpSystem;
        private UnitMovementSystem _unitMovementSystem;
        
        public override WarSide WarSide => Model.WarSide;
        public override float CurrentHealth { get => Model.CurrentHealth; set => Model.CurrentHealth = value; }

        protected void FixedUpdate()
        {
            View.UpdateHealthBar(Model.CurrentHealth, Model.MaxHealth);
            _detectionAim.DetectAim();
            _unitMovementSystem.MoveToAim();
            _damageSystem.Attack();
        }

        public override void Initialize()
        {
            InjectManager.Inject(this);

            View.Initialize();
            HeightObject = View.GetHeightObject();
            Model.NoAimPos = transform.position;
            ObjectsRegistry.Register(this);
            _unitMovementSystem = new UnitMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHpSystem(Model, View);
            Model.WayToAim = new List<Vector3> { transform.position };
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is WarriorFriendModel unitModel)
            {
                Model = unitModel;
                Initialize();
            }
        }

        public override void Restore()
        {
            transform.SetParent(null);
            Model.CurrentHealth = Model.MaxHealth;
            Model.NoAimPos = transform.position;
            CharacterPool.Remove(this);
            gameObject.SetActive(true);
        }

        public override void ReturnToPool()
        {
            CharacterPool.Return(this);
            ObjectsRegistry.Unregister(this);
            OnKilled?.Invoke(this);
        }

        public override void ClearData()
        {
            OnKilled = null;
            ObjectsRegistry.Unregister(this);
            _regenerationHpSystem.Dispose();
        }
    }
}