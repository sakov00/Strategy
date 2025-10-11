using _General.Scripts._VContainer;
using _General.Scripts.Extentions;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Interfaces.Model;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.ActionSystems
{
    public class DetectionAim
    {
        [Inject] private LiveRegistry _liveRegistry;
        
        private readonly IFightModel _fightModel;
        private readonly Transform _transform;

        public DetectionAim(IFightModel fightModel, Transform transform)
        {
            _fightModel = fightModel;
            _transform = transform;

            InjectManager.Inject(this);
        }

        public void DetectAim()
        {
            ObjectController nearestTarget = null;
            var nearestDistanceSqr = _fightModel.DetectionRadius;

            foreach (var obj in _liveRegistry.GetAllReactive())
            {
                if (obj == null || obj.CurrentHealth <= 0 ||
                    obj.WarSide == _fightModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(_transform, obj.transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = obj;
                }
            }

            _fightModel.AimObject = nearestTarget;
        }
    }
}