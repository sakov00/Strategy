using _Project.Scripts._VContainer;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public abstract class ObjectController: MonoBehaviour
    {
        public abstract ObjectModel ObjectModel { get; }
        public abstract ObjectView ObjectView { get; }

        protected virtual void Initialize()
        {
            InjectManager.Inject(this);
            ObjectView.Initialize();
            ObjectModel.Transform = transform;
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
    }
}