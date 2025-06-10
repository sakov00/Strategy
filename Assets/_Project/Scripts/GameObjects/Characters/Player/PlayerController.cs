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
    public class PlayerController : MonoBehaviour, IFightObject
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private HealthBarView healthBarView;
        
        private PlayerMovementSystem playerMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        public Transform Transform => transform;
        public WarSide WarSide => playerModel.warSide;
        public float MaxHealth => playerModel.maxHealth;
        public float CurrentHealth
        {
            get => playerModel.currentHealth;
            set => playerModel.currentHealth = value;
        }
        
        public float AttackRange
        {
            get => playerModel.attackRange;
            set => playerModel.attackRange = value;
        }
        public int DamageAmount         
        {
            get => playerModel.damageAmount;
            set => playerModel.damageAmount = value;
        }
        public float DelayAttack
        {
            get => playerModel.delayAttack;
            set => playerModel.delayAttack = value;
        }
        public float DetectionRadius        
        {
            get => playerModel.detectionRadius;
            set => playerModel.detectionRadius = value;
        }
        public TypeAttack TypeAttack
        {
            get => playerModel.typeAttack;
            set => playerModel.typeAttack = value;
        }
        
        public IDamagable AimCharacter
        {
            get => playerModel.AimCharacter;
            set => playerModel.AimCharacter = value;
        }

        private void Awake()
        {
            playerMovementSystem = new PlayerMovementSystem(playerModel, playerView, transform);
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, playerView, transform);
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