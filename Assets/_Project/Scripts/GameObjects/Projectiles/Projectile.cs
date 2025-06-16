using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [Inject] private HealthRegistry healthRegistry;
        
        public int damage;
        public WarSide ownerWarSide;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ObjectController>(out var target))
            {
                Destroy(gameObject);
                return;
            }

            if (target.Model.WarSide == ownerWarSide)
                return;

            target.Model.CurrentHealth -= damage;

            if (target.Model.CurrentHealth <= 0)
            {
                if (target.Model.WarSide == WarSide.Enemy)
                {
                    healthRegistry.Unregister(target.Model);
                    Destroy(target.Model.Transform.gameObject);
                }
                else
                {
                    target.gameObject.SetActive(false);
                }
            }

            Destroy(gameObject); 
        }
    }
}