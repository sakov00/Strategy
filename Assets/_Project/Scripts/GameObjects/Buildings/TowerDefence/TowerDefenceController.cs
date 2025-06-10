using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [RequireComponent(typeof(HealthBarView))]
    public class TowerDefenceController : MonoBehaviour, IFightObject
    {
        [SerializeField] private TowerDefenceModel towerDefenceModel;
        [SerializeField] private HealthBarView healthBarView;
        
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;
        
        public Transform Transform => transform;
        public WarSide WarSide => towerDefenceModel.warSide;
        public float MaxHealth => towerDefenceModel.maxHealth;
        public float CurrentHealth
        {
            get => towerDefenceModel.currentHealth;
            set => towerDefenceModel.currentHealth = value;
        }

        public IDamagable AimCharacter
        {
            get => towerDefenceModel.AimCharacter;
            set => towerDefenceModel.AimCharacter = value;
        }
        
        private void Awake()
        {
            detectionAim = new DetectionAim(this, transform);
            damageSystem = new DamageSystem(towerDefenceModel, playerView, transform);
        }

        private void Update()
        {            
            detectionAim.DetectAim();
            damageSystem.Attack();
            healthBarView.UpdateView();
        }
    }
}