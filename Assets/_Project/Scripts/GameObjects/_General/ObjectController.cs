using System;
using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class ObjectController : MonoBehaviour
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