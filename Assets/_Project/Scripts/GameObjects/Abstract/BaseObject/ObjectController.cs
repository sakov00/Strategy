using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract.BaseObject
{
    public abstract class ObjectController : MonoBehaviour, ISavableController, IPoolableDispose, IId
    {
        [Inject] protected AppData AppData;
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        [Inject] protected IdsRegistry IdsRegistry;
        
        protected abstract ObjectModel ObjectModel { get; }
        protected abstract ObjectView ObjectView { get; }
        
        public int Id { get => ObjectModel.Id; set => ObjectModel.Id = value; }
        public float HeightObject { get; protected set; }
        public WarSide WarSide => ObjectModel.WarSide;
        public float CurrentHealth { get => ObjectModel.CurrentHealth; set => ObjectModel.CurrentHealth = value; }

        private void Awake()
        {
            HeightObject = ObjectView.GetHeightObject();
            InjectManager.Inject(this);
            if (AppData.AppMode == AppMode.Redactor)
                ObjectsRegistry.Register(this);
        }

        protected virtual void FixedUpdate()
        {
            if (AppData.AppMode == AppMode.Redactor)
                return;
            ObjectView.UpdateHealthBar(ObjectModel.CurrentHealth, ObjectModel.MaxHealth);
        }

        private void OnDestroy()
        {
            Dispose(false);
            if (AppData.AppMode == AppMode.Redactor)
                ObjectsRegistry.Unregister(this);
        }

        public abstract void Initialize();
        public abstract ISavableModel GetSavableModel();
        public abstract void SetSavableModel(ISavableModel model);
        public abstract void Dispose(bool returnToPool = true, bool clearFromRegistry = true);
    }
}