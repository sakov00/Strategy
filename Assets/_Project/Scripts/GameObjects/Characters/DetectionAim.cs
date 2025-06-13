using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Extentions;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DetectionAim
    {
        private readonly IFightObjectModel fightObjectModel;
        private readonly Transform transform;

        public DetectionAim(IFightObjectModel fightObjectModel, Transform transform)
        {
            this.fightObjectModel = fightObjectModel;
            this.transform = transform;
        }

        public void DetectAim()
        {
            IHealthModel nearestTarget = null;
            var nearestDistanceSqr = float.MaxValue;

            foreach (var damagable in GlobalObjects.GameData.allDamagables)
            {
                if (damagable == null || ReferenceEquals(damagable, fightObjectModel) || damagable.WarSide == fightObjectModel.WarSide)
                    continue;

                var distance = PositionExtention.GetDistanceBetweenObjects(transform, damagable.Transform);
                if (distance < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distance;
                    nearestTarget = damagable;
                }
            }

            fightObjectModel.AimCharacter = nearestTarget;
        }
    }
}
