using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(HealthBarView))]
    public class UnitController : MonoBehaviour, IFightObject
    {
        [SerializeField] private UnitModel unitModel;
        [SerializeField] private UnitView unitView;
        [SerializeField] private HealthBarView healthBarView;
        
        private UnitMovementSystem unitMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        public Transform Transform => transform;
        public WarSide WarSide => unitModel.warSide;
        public float MaxHealth => unitModel.maxHealth;
        public float CurrentHealth
        {
            get => unitModel.currentHealth;
            set => unitModel.currentHealth = value;
        }

        public float AttackRange
        {
            get => unitModel.attackRange;
            set => unitModel.attackRange = value;
        }
        public int DamageAmount         
        {
            get => unitModel.damageAmount;
            set => unitModel.damageAmount = value;
        }
        public float DelayAttack
        {
            get => unitModel.delayAttack;
            set => unitModel.delayAttack = value;
        }
        public float DetectionRadius        
        {
            get => unitModel.detectionRadius;
            set => unitModel.detectionRadius = value;
        }
        public TypeAttack TypeAttack
        {
            get => unitModel.typeAttack;
            set => unitModel.typeAttack = value;
        }
        
        public IDamagable AimCharacter
        {
            get => unitModel.AimCharacter;
            set => unitModel.AimCharacter = value;
        }
        
        private void Awake()
        {
            unitMovementSystem = new UnitMovementSystem(unitModel, unitView, transform);
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, unitView, transform);
        }

        private void FixedUpdate()
        {
            detectionAim.DetectAim();
            unitMovementSystem.MoveToAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }

        public void SetNoAimPosition(Vector3 position)
        {
            unitModel.noAimPosition = position;
        }
    }
}