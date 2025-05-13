using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Models.Characters;
using UnityEngine;

namespace _Project.Scripts.Models
{
    public class Projectile : MonoBehaviour
    {
        public int damage;
        public WarSide ownerWarSide;

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<IDamagable>();
            if (target != null && target.WarSide != ownerWarSide)
            {
                target.CurrentHealth -= damage;
                if (target.CurrentHealth <= 0)
                    Destroy(target.Transform.gameObject);

                Destroy(gameObject);
            }
        }
    }
}