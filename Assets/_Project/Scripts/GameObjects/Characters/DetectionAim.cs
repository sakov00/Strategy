using _Project.Scripts.Extentions;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [RequireComponent(typeof(IFightObject))]
    public class DetectionAim : MonoBehaviour
    {
        private IFightObject attackableModel;

        private void Awake()
        {
            attackableModel ??= GetComponent<IFightObject>();
        }

        public void DetectAim()
        {
            var hitBuffer = Physics.OverlapSphere(transform.position, attackableModel.DetectionRadius);
            IDamagable nearestTarget = null;
            var nearestDistanceSqr = float.MaxValue;

            foreach (var hit in hitBuffer)
            {
                var damagable = hit.GetComponent<IDamagable>();
                if (damagable == null || ReferenceEquals(damagable, attackableModel) || damagable.WarSide == attackableModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(transform, hit.transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = damagable;
                }
            }

            attackableModel.AimCharacter = nearestTarget;
        }
    }
}
