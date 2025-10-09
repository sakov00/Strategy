using UnityEngine;

namespace _Project.Scripts.GameObjects.Additional.Projectiles
{
    public class Arrow : Projectile
    {
        [Header("Flight Settings")] 
        [SerializeField] private Rigidbody _rb;

        private void OnValidate()
        {
            _rb ??= GetComponent<Rigidbody>();
        }

        public override void LaunchToPoint(Vector3 targetPosition, float initialSpeed)
        {
            var dir = (targetPosition - transform.position).normalized;
            _rb.linearVelocity = dir * initialSpeed;
        }
    }
}