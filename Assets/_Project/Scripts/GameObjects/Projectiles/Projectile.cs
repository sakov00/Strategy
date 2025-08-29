using System;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Inject] protected ObjectsRegistry _objectsRegistry;
        
        public int _damage;
        public WarSide _ownerWarSide;
        public abstract void LaunchToPoint(Vector3 targetPosition, float initialSpeed);

        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ObjectController>();

            if (target == null || target.ObjectModel.WarSide == _ownerWarSide)
                return;

            target.ObjectModel.CurrentHealth -= _damage;

            if (target.ObjectModel.CurrentHealth <= 0)
            {
                    _objectsRegistry.Unregister(target);
                    Destroy(target.ObjectModel.Transform.gameObject);
                    //target.gameObject.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}