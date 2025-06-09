using _Project.Scripts._GlobalLogic;
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
            IDamagable nearestTarget = null;
            var nearestDistanceSqr = float.MaxValue;

            foreach (var damagable in GlobalObjects.GameData.allDamagables)
            {
                if (damagable == null || ReferenceEquals(damagable, attackableModel) || damagable.WarSide == attackableModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(transform, damagable.Transform);
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
