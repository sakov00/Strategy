using _General.Scripts.AllAppData;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Units.Character;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Units.Friends.Player
{
    public class PlayerController : MyCharacterController, IJsonSerializable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;

        [field:SerializeField] private PlayerModel Model { get; set; }
        [field:SerializeField] private PlayerView View { get; set; }
        public override CharacterModel CharacterModel => Model;
        public override CharacterView CharacterView => View;
        
        private PlayerMovementSystem _playerMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHPSystem _regenerationHpSystem;
        
        protected override void Initialize()
        {
            base.Initialize();
            _playerMovementSystem = new PlayerMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHPSystem(Model, View);
            
            _objectsRegistry.Register(this);
        }

        private void Update()
        {
            _playerMovementSystem.MoveTo(AppData.LevelData.MoveDirection);
        }
        
        public override ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(PlayerController),
                position = transform.position,
                rotation = transform.rotation
            };
            return objectJson;
        }

        public override void SetJsonData(ObjectJson objectJson)
        {
            transform.position = objectJson.position;
            transform.rotation = objectJson.rotation;
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public override void ClearData()
        {
            _regenerationHpSystem.Dispose();
            Destroy(gameObject);
        }
    }
}