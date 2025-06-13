using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    public class UnitController : CharacterSimpleController
    {
        [SerializeField] private UnitModel model;
        [SerializeField] private UnitView view;

        private UnitMovementSystem unitMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        protected override void Initialize()
        {
            CharacterModel = model;
            CharacterView = view;
            
            unitMovementSystem = new UnitMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);

            base.Initialize();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            detectionAim.DetectAim();
            unitMovementSystem.MoveToAim();
            damageSystem.Attack();
        }

        public void SetNoAimPosition(Vector3 position)
        {
            model.noAimPosition = position;
        }
    }
}