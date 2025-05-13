using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Models
{
    public class Projectile : MonoBehaviour
    {
        public int damage;
        public WarSide ownerWarSide;

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<CharacterModel>();
            if (target != null && target.warSide != ownerWarSide)
            {
                target.currentHealth -= damage;
                if (target.currentHealth <= 0)
                    Destroy(target.gameObject);

                Destroy(gameObject);
            }
        }
    }
}