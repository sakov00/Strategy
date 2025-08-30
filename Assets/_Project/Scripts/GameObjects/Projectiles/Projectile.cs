using System;
using System.Collections;
using System.Collections.Generic;
using _General.Scripts._VContainer;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Inject] private ProjectilePool _projectilePool;
        [field:SerializeField] public ProjectileType ProjectileType { get; set; }
        
        public int _damage;
        public WarSide _ownerWarSide;
        public abstract void LaunchToPoint(Vector3 targetPosition, float initialSpeed);

        private void Start()
        {
            InjectManager.Inject(this);
            Invoke(nameof(ReturnToPool), 5f);
        }

        private void ReturnToPool()
        {
            _projectilePool.Return(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ObjectController>();

            if (target == null || target.ObjectModel.WarSide == _ownerWarSide)
                return;

            target.ObjectModel.CurrentHealth -= _damage;

            if (target.ObjectModel.CurrentHealth <= 0)
            {
                target.ReturnToPool();
            }

            _projectilePool.Return(this);
        }
    }
}