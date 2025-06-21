using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    public class UnitController : MyCharacterController
    {
        [SerializeField] private UnitModel model;
        [SerializeField] private UnitView view;
        protected override CharacterModel CharacterModel => model;
        protected override CharacterView CharacterView => view;

        private UnitMovementSystem unitMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        protected override void Initialize()
        {
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