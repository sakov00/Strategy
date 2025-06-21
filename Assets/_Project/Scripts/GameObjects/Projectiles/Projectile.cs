using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
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

            if (target.ObjectModel.WarSide == ownerWarSide)
                return;

            target.ObjectModel.CurrentHealth -= damage;

            if (target.ObjectModel.CurrentHealth <= 0)
            {
                if (target.ObjectModel.WarSide == WarSide.Enemy)
                {
                    healthRegistry.Unregister(target.ObjectModel);
                    Destroy(target.ObjectModel.Transform.gameObject);
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