using _General.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Additional.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Inject] private ProjectilePool _projectilePool;
        [field: SerializeField] public ProjectileType ProjectileType { get; set; }

        public int _damage;
        public WarSide _ownerWarSide;

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

            if (target != null && target.WarSide != _ownerWarSide)
            {
                target.CurrentHealth -= _damage;
                ReturnToPool();
                if (target.CurrentHealth <= 0) target.Dispose();
            }
            else if (target != null)
            {
                return;
            }

            ReturnToPool();
        }

        public abstract void LaunchToPoint(Vector3 targetPosition, float initialSpeed);

        private void ReturnToPool()
        {
            CancelInvoke(nameof(ReturnToPool));
            _projectilePool.Return(this);
        }
    }
}