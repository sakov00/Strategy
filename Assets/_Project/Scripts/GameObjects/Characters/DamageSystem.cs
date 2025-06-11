using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Extentions;
using _Project.Scripts.GameObjects.Projectiles;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DamageSystem
    {
        private readonly IFightObject fightObject;
        private readonly IProjectTileView projectTileView;
        private readonly Transform transform;
        private float lastAttackTime = -Mathf.Infinity;

        public DamageSystem(IFightObject fightObject, IProjectTileView projectTileView, Transform transform)
        {
            this.fightObject = fightObject;
            this.projectTileView = projectTileView;
            this.transform = transform;
        }

        public void Attack()
        {
            if (fightObject.AimCharacter == null || fightObject.AimCharacter.Equals(null))
                return;
            
            if (Time.time - lastAttackTime < fightObject.DelayAttack)
                return;

            if (fightObject.TypeAttack == TypeAttack.Melee)
                MeleeAttack();
            if (fightObject.TypeAttack == TypeAttack.Distance)
                DistanceAttack();
        }

        private void MeleeAttack()
        {
            var distance =
                PositionExtention.GetDistanceBetweenObjects(transform, fightObject.AimCharacter.Transform);
            if (distance <= fightObject.AttackRange)
            {
                lastAttackTime = Time.time;
                fightObject.AimCharacter.CurrentHealth -= fightObject.DamageAmount;
                if (fightObject.AimCharacter.CurrentHealth <= 0)
                {
                    GlobalObjects.GameData.allDamagables.Remove(fightObject.AimCharacter);
                    Object.Destroy(fightObject.AimCharacter.Transform.gameObject);
                }
            }
        }
        
        private void DistanceAttack()
        {
            var distance = Vector3.Distance(transform.position, fightObject.AimCharacter.Transform.position);
            if (distance <= fightObject.AttackRange)
            {
                lastAttackTime = Time.time;
                ShootProjectile();
            }
        }
        
        private void ShootProjectile()
        {
            if (projectTileView.ProjectilePrefab == null || projectTileView.FirePoint == null || fightObject.AimCharacter == null)
                return;

            GameObject projectile = Object.Instantiate(projectTileView.ProjectilePrefab, projectTileView.FirePoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null)
                return;

            var velocity = CalculateBallisticVelocityVector(projectTileView.FirePoint.position, 
                fightObject.AimCharacter.Transform.position, projectTileView.ProjectileSpeed);
            if (velocity.HasValue)
            {
                rb.useGravity = true;
                rb.velocity = velocity.Value;

                var damageComponent = projectile.GetComponent<Projectile>();
                if (damageComponent != null)
                {
                    damageComponent.damage = fightObject.DamageAmount;
                    damageComponent.ownerWarSide = fightObject.WarSide;
                }
            }
            else
            {
                Debug.LogWarning("Цель вне досягаемости с заданной скоростью!");
                Object.Destroy(projectile); // не тратить лишние объекты
            }
        }
        
        private Vector3? CalculateBallisticVelocityVector(Vector3 start, Vector3 target, float launchSpeed)
        {
            Vector3 toTarget = target - start;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

            float y = toTarget.y;
            float xz = toTargetXZ.magnitude;

            float gravity = Mathf.Abs(Physics.gravity.y);
            float speed2 = launchSpeed * launchSpeed;
            float underSqrt = speed2 * speed2 - gravity * (gravity * xz * xz + 2 * y * speed2);

            if (underSqrt < 0)
                return null; // Цель вне досягаемости с данным launchSpeed

            float sqrt = Mathf.Sqrt(underSqrt);

            // Выбери угол: +sqrt = выше, -sqrt = ниже
            float lowAngle = Mathf.Atan2(speed2 - sqrt, gravity * xz);
            // float highAngle = Mathf.Atan2(speed2 + sqrt, gravity * xz); // альтернатива, более навесная

            float angle = lowAngle;

            Vector3 velocity = toTargetXZ.normalized * Mathf.Cos(angle) * launchSpeed;
            velocity.y = Mathf.Sin(angle) * launchSpeed;

            return velocity;
        }
    }
}
