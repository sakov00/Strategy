using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public int damage;
        public WarSide ownerWarSide;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamagable>(out var target))
            {
                Destroy(gameObject);
                return;
            }

            if (target.WarSide == ownerWarSide)
                return;

            target.CurrentHealth -= damage;

            if (target.CurrentHealth <= 0)
            {
                Destroy(target.Transform.gameObject);
            }

            Destroy(gameObject); 
        }
    }
}