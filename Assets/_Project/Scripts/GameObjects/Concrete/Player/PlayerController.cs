using _General.Scripts.AllAppData;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
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
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private PlayerMovementSystem _playerMovementSystem;
        private RegenerationHpSystem _regenerationHpSystem;
        
        public override WarSide WarSide => Model.WarSide;
        public override float CurrentHealth { get => Model.CurrentHealth; set => Model.CurrentHealth = value; }

        private void Update()
        {
            _playerMovementSystem.MoveTo(_appData.LevelData.MoveDirection);
        }

        protected void FixedUpdate()
        {
            View.UpdateHealthBar(Model.CurrentHealth, Model.MaxHealth);
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }

        public override void Initialize()
        {
            View.Initialize();
            HeightObject = View.GetHeightObject();
            Model.NoAimPos = transform.position;
            ObjectsRegistry.Register(this);
            _playerMovementSystem = new PlayerMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHpSystem(Model, View);
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

        public override void Restore()
        {
            transform.SetParent(null);
            Model.CurrentHealth = Model.MaxHealth;
            Model.NoAimPos = transform.position;
            CharacterPool.Remove(this);
            gameObject.SetActive(true);
            ObjectsRegistry.Register(this);
        }

        public override void ReturnToPool()
        {
            CharacterPool.Return(this);
        }

        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
            _regenerationHpSystem.Dispose();
        }
    }
}