using _General.Scripts.AllAppData;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerController : MyCharacterController, ISavable<PlayerJson>
    {
        [Inject] private ObjectsRegistry _objectsRegistry;

        [SerializeField] private PlayerModel _model;
        [SerializeField] private PlayerView _view;
        protected override CharacterModel CharacterModel => _model;
        protected override CharacterView CharacterView => _view;
        
        private PlayerMovementSystem _playerMovementSystem;
        private DetectionAim _detectionAim;
        private DamageSystem _damageSystem;
        private RegenerationHPSystem _regenerationHpSystem;
        
        protected override void Initialize()
        {
            base.Initialize();
            _playerMovementSystem = new PlayerMovementSystem(_model, _view, transform);
            _detectionAim = new DetectionAim(_model, transform);
            _damageSystem = new DamageSystem(_model, _view, transform);
            _regenerationHpSystem = new RegenerationHPSystem(_model, _view);
            
            _objectsRegistry.Register(this);
        }
        
        public PlayerJson GetJsonData()
        {
            var playerJson = new PlayerJson
            {
                position = transform.position,
                rotation = transform.rotation,
                playerModel = _model
            };
            return playerJson;
        }

        public void SetJsonData(PlayerJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _model = environmentJson.playerModel;
        }

        private void Update()
        {
            _playerMovementSystem.MoveTo(AppData.LevelData.MoveDirection);
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }
        
        public void ClearData()
        {
            _regenerationHpSystem.Dispose();
            Destroy(gameObject);
        }
    }
}