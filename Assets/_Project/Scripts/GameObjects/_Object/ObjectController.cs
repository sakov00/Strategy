using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object
{
    public abstract class ObjectController : MonoBehaviour, ISavableController, IClearData
    {
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        
        public abstract ObjectModel ObjectModel { get; }
        public abstract ObjectView ObjectView { get; }

        public virtual void Initialize()
        {
            InjectManager.Inject(this);
            ObjectView.Initialize();
            ObjectModel.HeightObject = ObjectView.GetHeightObject();
            ObjectModel.NoAimPos = transform.position;
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void FixedUpdate()
        {
            ObjectView.UpdateHealthBar(ObjectModel.CurrentHealth, ObjectModel.MaxHealth);
        }
        
        public abstract ISavableModel GetSavableModel();
        public abstract void SetSavableModel(ISavableModel model);
        public abstract void Restore();
        public abstract void ReturnToPool();
        public abstract void ClearData();

        private void OnDestroy()
        {
            ClearData();
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}