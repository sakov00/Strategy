using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    [RequireComponent(typeof(HealthBarView))]
    public class PlayerController : CharacterSimpleController, IFightObject
    {
        [SerializeField] private PlayerModel model;
        [SerializeField] private PlayerView view;
        [SerializeField] private HealthBarView healthBarView;
        
        private PlayerMovementSystem playerMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;
        
        public float AttackRange
        {
            get => model.attackRange;
            set => model.attackRange = value;
        }
        public int DamageAmount         
        {
            get => model.damageAmount;
            set => model.damageAmount = value;
        }
        public float DelayAttack
        {
            get => model.delayAttack;
            set => model.delayAttack = value;
        }
        public float DetectionRadius        
        {
            get => model.detectionRadius;
            set => model.detectionRadius = value;
        }
        public TypeAttack TypeAttack
        {
            get => model.typeAttack;
            set => model.typeAttack = value;
        }
        
        public IDamagable AimCharacter
        {
            get => model.AimCharacter;
            set => model.AimCharacter = value;
        }

        private void Awake()
        {
            CharacterModel = model;
            CharacterView = view;
            
            playerMovementSystem = new PlayerMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, view, transform);
        }

        private void Update()
        {
            var inputVector = new Vector3(
                GlobalObjects.GameData.gameWindow.joystick.Direction.x,
                0,
                GlobalObjects.GameData.gameWindow.joystick.Direction.y);
            
            playerMovementSystem.MoveTo(inputVector);
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}