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
        [SerializeField] protected TowerDefenceModel towerDefenceModel;
        [SerializeField] protected TowerDefenceView towerDefenceView;
        [SerializeField] private HealthBarView healthBarView;
        
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        public float AttackRange
        {
            get => towerDefenceModel.attackRange;
            set => towerDefenceModel.attackRange = value;
        }
        public int DamageAmount         
        {
            get => towerDefenceModel.damageAmount;
            set => towerDefenceModel.damageAmount = value;
        }
        public float DelayAttack
        {
            get => towerDefenceModel.delayAttack;
            set => towerDefenceModel.delayAttack = value;
        }
        public float DetectionRadius        
        {
            get => towerDefenceModel.detectionRadius;
            set => towerDefenceModel.detectionRadius = value;
        }
        public TypeAttack TypeAttack
        {
            get => towerDefenceModel.typeAttack;
            set => towerDefenceModel.typeAttack = value;
        }
        
        public IDamagable AimCharacter
        {
            get => towerDefenceModel.AimCharacter;
            set => towerDefenceModel.AimCharacter = value;
        }
        
        private void Awake()
        {
            BuildModel = towerDefenceModel;
            BuildView = towerDefenceView;
            
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(this, towerDefenceView, transform);
        }

        private void Update()
        {            
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}