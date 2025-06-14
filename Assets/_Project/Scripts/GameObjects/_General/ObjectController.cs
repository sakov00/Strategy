using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public abstract class ObjectController : MonoBehaviour
    {
        public ObjectModel Model { get; protected set; }
        public ObjectView View { get; protected set; }

        protected virtual void Initialize()
        {
            View.Initialize();
            Model.Transform = transform;
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void FixedUpdate()
        {
            View.UpdateHealthBar(Model.CurrentHealth, Model.MaxHealth);
        }
    }
}