using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Extentions;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DetectionAim
    {
        [Inject] private HealthRegistry healthRegistry;
        
        private readonly IFightObjectModel fightObjectModel;
        private readonly Transform transform;

        public DetectionAim(IFightObjectModel fightObjectModel, Transform transform)
        {
            this.fightObjectModel = fightObjectModel;
            this.transform = transform;
            
            InjectManager.Inject(this);
        }

        public void DetectAim()
        {
            IHealthModel nearestTarget = null;
            var nearestDistanceSqr = fightObjectModel.DetectionRadius;

            foreach (var healthModel in healthRegistry.GetAll())
            {
                if (healthModel == null || healthModel.CurrentHealth <= 0 || 
                    ReferenceEquals(healthModel, fightObjectModel) || healthModel.WarSide == fightObjectModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(transform, healthModel.Transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = healthModel;
                }
            }

            fightObjectModel.AimCharacter = nearestTarget;
        }
    }
}
