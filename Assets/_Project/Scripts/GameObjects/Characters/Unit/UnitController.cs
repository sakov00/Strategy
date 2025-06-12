using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(HealthBarView))]
    public class UnitController : CharacterSimpleController, IFightObject
    {
        [SerializeField] private UnitModel model;
        [SerializeField] private UnitView view;
        [SerializeField] private HealthBarView healthBarView;
        
        private UnitMovementSystem unitMovementSystem;
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
            
            unitMovementSystem = new UnitMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, view, transform);
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
            model.noAimPosition = position;
        }
    }
}