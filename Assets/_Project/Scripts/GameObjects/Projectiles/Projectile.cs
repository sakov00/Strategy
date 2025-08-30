using System;
using _General.Scripts._VContainer;
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
        }

        private void OnEnable()
        {
            CancelInvoke(nameof(ReturnToPool));
            Invoke(nameof(ReturnToPool), 5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ObjectController>();

            if (target != null && target.ObjectModel.WarSide != _ownerWarSide)
            {
                target.ObjectModel.CurrentHealth -= _damage;
                ReturnToPool();
                if (target.ObjectModel.CurrentHealth <= 0)
                {
                    target.ReturnToPool();
                }
            }
            else if (target != null)
            {
                return;
            }

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            CancelInvoke(nameof(ReturnToPool));
            _projectilePool.Return(this);
        }
    }
}