using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.ActionSystems;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MainBuild
{
    public class MainBuildController : BuildController
    {
        [field: SerializeField] public MainBuildModel Model { get; private set; }
        [field: SerializeField] public MainBuildingView View { get; private set; }
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        public override WarSide WarSide => Model.WarSide;
        public override float CurrentHealth { get => Model.CurrentHealth; set => Model.CurrentHealth = value; }

        protected void FixedUpdate()
        {
            View.UpdateHealthBar(Model.CurrentHealth, Model.MaxHealth);
            _detectionAim.DetectAim();
            _damageSystem.Attack();
        }

        public override void Initialize()
        {
            InjectManager.Inject(this);

            View.Initialize();
            HeightObject = View.GetHeightObject();
            Model.NoAimPos = transform.position;
            ObjectsRegistry.Register(this);
            
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is MainBuildModel moneyBuildModel)
            {
                Model = moneyBuildModel;
                Initialize();
            }
        }

        public override void Restore()
        {
            transform.SetParent(null);
            Model.CurrentHealth = Model.MaxHealth;
            Model.NoAimPos = transform.position;
            BuildPool.Remove(this);
            gameObject.SetActive(true);
            ObjectsRegistry.Register(this);
        }

        public override void ReturnToPool()
        {
            ClearData();
            BuildPool.Return(this);
        }

        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
        }
    }
}