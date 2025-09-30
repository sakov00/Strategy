using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract.BaseObject
{
    public abstract class ObjectController : MonoBehaviour, ISavableController, IClearData
    {
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        
        public float HeightObject { get; protected set; }
        public abstract WarSide WarSide { get; }
        public abstract float CurrentHealth { get; set; }

        protected virtual void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            ClearData();
        }

        public abstract void ClearData();

        public abstract ISavableModel GetSavableModel();
        public abstract void SetSavableModel(ISavableModel model);

        public abstract void Initialize();
        public abstract void Restore();
        public abstract void ReturnToPool();
    }
}