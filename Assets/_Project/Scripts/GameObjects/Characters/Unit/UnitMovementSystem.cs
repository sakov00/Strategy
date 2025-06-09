using _Project.Scripts.Extentions;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [RequireComponent(typeof(UnitModel))]
    public class UnitMovementSystem : MonoBehaviour
    {
        [SerializeField, HideInInspector] private UnitModel unitModel;
        private Vector3 velocity;

        private void OnValidate()
        {
            unitModel ??= GetComponent<UnitModel>();
        }

        public void MoveToAim()
        {
            if (unitModel.AimCharacter == null || unitModel.AimCharacter.Equals(null))
            {
                unitModel.agent.SetDestination(unitModel.NoAimPosition);
                unitModel.agent.isStopped = false;
                return;
            }
  
            var distance = PositionExtention.GetDistanceBetweenObjects(transform, unitModel.AimCharacter.Transform);
            unitModel.agent.isStopped = distance < unitModel.AttackRange;
            unitModel.agent.SetDestination(unitModel.AimCharacter.Transform.position);
        }
    }
}