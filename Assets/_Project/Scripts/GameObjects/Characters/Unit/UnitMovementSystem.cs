using _Project.Scripts.Extentions;
using _Project.Scripts.GameObjects.Characters.Player;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    public class UnitMovementSystem
    {
        private readonly UnitModel unitModel;
        private readonly UnitView unitView;
        private readonly Transform transform;
        private Vector3 velocity;
        
        public UnitMovementSystem(UnitModel unitModel, UnitView unitView, Transform transform)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;
            this.transform = transform;
        }

        public void MoveToAim()
        {
            var thresholdSqr = 0.5f;
            if (unitModel.currentWaypointIndex < unitModel.wayToAim.Count - 1)
            {
                if ((transform.position - unitModel.wayToAim[unitModel.currentWaypointIndex]).sqrMagnitude < thresholdSqr)
                {
                    unitModel.currentWaypointIndex++;
                }
            }
            
            if (unitModel.AimCharacter == null || unitModel.AimCharacter.Equals(null))
            {
                unitView.Agent.SetDestination(unitModel.wayToAim[unitModel.currentWaypointIndex]);
                unitView.Agent.isStopped = false;
                return;
            }
  
            var distance = PositionExtention.GetDistanceBetweenObjects(transform, unitModel.AimCharacter.Transform);
            unitView.Agent.isStopped = distance < unitModel.AttackRange;
            unitView.Agent.SetDestination(unitModel.AimCharacter.Transform.position);
        }
    }
}