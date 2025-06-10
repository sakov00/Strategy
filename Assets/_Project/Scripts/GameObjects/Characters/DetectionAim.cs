using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Extentions;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DetectionAim
    {
        private readonly IFightObject attackableModel;
        private readonly Transform transform;

        public DetectionAim(IFightObject attackableModel, Transform transform)
        {
            this.attackableModel = attackableModel;
            this.transform = transform;
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
