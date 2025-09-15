using _General.Scripts.AllAppData;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.ActionSystems;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.Characters.Friends.Player
{
    public class PlayerController : MyCharacterController
    {
        [Inject] private AppData _appData;
        [field:SerializeField] public PlayerModel Model { get; private set; }
        [field:SerializeField] public PlayerView View  { get; private set; }
        public override CharacterModel CharacterModel => Model;
        public override CharacterView CharacterView => View;
        
        private PlayerMovementSystem _playerMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHpSystem _regenerationHpSystem;
        
        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
            _playerMovementSystem = new PlayerMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHpSystem(Model, View);
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
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public override void Restore()
        {
            base.Restore();
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