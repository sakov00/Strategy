using System;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract.BaseObject
{
    public abstract class ObjectController : MonoBehaviour, ISavableController, IClearScene
    {
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        
        protected abstract ObjectModel ObjectModel { get; }
        protected abstract ObjectView ObjectView { get; }
        
        public float HeightObject { get; protected set; }
        public WarSide WarSide => ObjectModel.WarSide;
        public float CurrentHealth { get => ObjectModel.CurrentHealth; set => ObjectModel.CurrentHealth = value; }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void FixedUpdate()
        {
            ObjectView.UpdateHealthBar(ObjectModel.CurrentHealth, ObjectModel.MaxHealth);
        }

        private void OnDestroy()
        {
            DeleteFromScene(true);
        }

        public virtual void Initialize()
        {
            HeightObject = ObjectView.GetHeightObject();
        }

        public abstract void DeleteFromScene(bool realDelete = false);
        public abstract ISavableModel GetSavableModel();
        public abstract void SetSavableModel(ISavableModel model);
    }
}