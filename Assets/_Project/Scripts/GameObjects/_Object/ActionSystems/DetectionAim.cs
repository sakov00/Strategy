using _General.Scripts._VContainer;
using _General.Scripts.Extentions;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces.Model;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.ActionSystems
{
    public class DetectionAim
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
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
            ObjectController nearestTarget = null;
            var nearestDistanceSqr = _fightObjectModel.DetectionRadius;

            foreach (var obj in _objectsRegistry.GetAllByInterface<ObjectController>())
            {
                if (obj == null || obj.ObjectModel.CurrentHealth <= 0 || obj.ObjectModel.WarSide == _fightObjectModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(_transform, obj.transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = obj;
                }
            }

            _fightObjectModel.AimObject = nearestTarget;
        }
    }
}
