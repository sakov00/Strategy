using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    public class UnitController : MyCharacterController, ISavable<UnitJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        
        [SerializeField] private UnitModel model;
        [SerializeField] private UnitView view;
        protected override CharacterModel CharacterModel => model;
        protected override CharacterView CharacterView => view;

        private UnitMovementSystem unitMovementSystem;
        private DetectionAim detectionAim;
        private DamageSystem damageSystem;

        protected override void Initialize()
        {
#if EDIT_MODE
            _saveRegistry.Register(this);
#endif
            
            unitMovementSystem = new UnitMovementSystem(model, view, transform);
            detectionAim = new DetectionAim(model, transform);
            damageSystem = new DamageSystem(model, view, transform);

            base.Initialize();
        }
        
        public UnitJson GetJsonData()
        {
            var unitJson = new UnitJson
            {
                position = transform.position,
                rotation = transform.rotation,
                unitModel = model
            };
            return unitJson;
        }

        public void SetJsonData(UnitJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            model = environmentJson.unitModel;
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
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}