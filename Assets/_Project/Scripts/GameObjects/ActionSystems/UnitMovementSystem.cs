using _General.Scripts.Extentions;
using _Project.Scripts.GameObjects.Abstract.Unit;
using UnityEngine;

namespace _Project.Scripts.GameObjects.ActionSystems
{
    public class UnitMovementSystem
    {
        private readonly Transform _transform;
        private readonly UnitModel _unitModel;
        private readonly UnitView _unitView;
        private Vector3 _velocity;

        public UnitMovementSystem(UnitModel unitModel, UnitView unitView, Transform transform)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _transform = transform;
        }

        public void MoveToAim()
        {
            if (!_unitView.Agent.isOnNavMesh) return;
            var thresholdSqr = 0.5f;
            if (_unitModel.CurrentWaypointIndex < _unitModel.WayToAim.Count - 1)
                if ((_transform.position - _unitModel.WayToAim[_unitModel.CurrentWaypointIndex]).sqrMagnitude <
                    thresholdSqr)
                    _unitModel.CurrentWaypointIndex++;

            if (_unitModel.AimObject == null || _unitModel.AimObject.Equals(null))
            {
                _unitView.Agent.SetDestination(_unitModel.WayToAim[_unitModel.CurrentWaypointIndex]);
                _unitView.Agent.isStopped = false;
                return;
            }

            var distance = PositionExtention.GetDistanceBetweenObjects(_transform, _unitModel.AimObject.transform,
                out var nearestPointA, out var nearestPointB);
            _unitView.Agent.isStopped = distance < _unitModel.AttackRange;
            _unitView.Agent.SetDestination(nearestPointB);
        }
    }
}