using System;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Projectiles
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
            Vector3 dir = (targetPosition - transform.position).normalized;
            _rb.velocity = dir * initialSpeed;
        }
    }
}