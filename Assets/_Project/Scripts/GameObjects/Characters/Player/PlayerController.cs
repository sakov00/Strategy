using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerController : MyCharacterController
    {
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
            playerMovementSystem = new PlayerMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);
            
            base.Initialize();
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