using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [RequireComponent(typeof(HealthBarView))]
    public class TowerDefenceController : BuildController, IFightObject
    {
        [SerializeField] protected TowerDefenceModel model;
        [SerializeField] protected TowerDefenceView view;
        
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
            BuildModel = model;
            BuildView = view;
            
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, view, transform);
        }

        private void FixedUpdate()
        {            
            detectionAim.DetectAim();
            damageSystem.Attack();
            view.UpdateHealthBar(model.currentHealth, model.maxHealth);
        }
    }
}