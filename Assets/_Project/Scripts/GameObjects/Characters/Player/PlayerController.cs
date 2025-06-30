using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerController : MyCharacterController, ISavable<PlayerJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private GameWindowViewModel gameWindowViewModel;

        [SerializeField] private PlayerModel model;
        [SerializeField] private PlayerView view;
        protected override CharacterModel CharacterModel => model;
        protected override CharacterView CharacterView => view;
        
        private PlayerMovementSystem playerMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;
        
        protected override void Initialize()
        {
            _saveRegistry.Register(this);
            playerMovementSystem = new PlayerMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);
            
            base.Initialize();
        }
        
        public PlayerJson GetJsonData()
        {
            var playerJson = new PlayerJson
            {
                position = transform.position,
                rotation = transform.rotation,
                playerModel = model
            };
            return playerJson;
        }

        public void SetJsonData(PlayerJson playerJson)
        {
            transform.position = playerJson.position;
            transform.rotation = playerJson.rotation;
            model = playerJson.playerModel;
        }

        private void Update()
        {
            playerMovementSystem.MoveTo(gameWindowViewModel.MoveDirection);
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            detectionAim.DetectAim();
            damageSystem.Attack();
        }
    }
}