using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    public class TowerDefenceController : BuildController
    {
        [SerializeField] protected TowerDefenceModel model;
        [SerializeField] protected TowerDefenceView view;
        public override BuildModel BuildModel => model;
        public override BuildView BuildView => view;
        
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        protected override void Initialize()
        {
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);
            
            base.Initialize();
        }

        protected override void FixedUpdate()
        {            
            base.FixedUpdate();
            detectionAim.DetectAim();
            damageSystem.Attack();
        }
    }
}