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
        [Inject] private HealthRegistry _healthRegistry;
        
        private readonly IFightObjectModel _fightObjectModel;
        private readonly Transform _transform;

        public DetectionAim(IFightObjectModel fightObjectModel, Transform transform)
        {
            _fightObjectModel = fightObjectModel;
            _transform = transform;
            
            InjectManager.Inject(this);
        }

        public void DetectAim()
        {
            IHealthModel nearestTarget = null;
            var nearestDistanceSqr = _fightObjectModel.DetectionRadius;

            foreach (var healthModel in _healthRegistry.GetAll())
            {
                if (healthModel == null || healthModel.CurrentHealth <= 0 || 
                    ReferenceEquals(healthModel, _fightObjectModel) || healthModel.WarSide == _fightObjectModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(_transform, healthModel.Transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = healthModel;
                }
            }

            _fightObjectModel.AimCharacter = nearestTarget;
        }
    }
}
