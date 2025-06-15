using System;
using System.Collections;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Extentions;
using _Project.Scripts.GameObjects.Projectiles;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameObjects.Characters
{
    public class DamageSystem
    {
        private readonly IFightObjectModel fightObjectModel;
        private readonly IAttackableView attackableView;
        private readonly Transform transform;
        
        private float lastAttackTime = -Mathf.Infinity;
        private Coroutine attackCoroutine;

        public DamageSystem(IFightObjectModel fightObjectModel, IAttackableView attackableView, Transform transform)
        {
            this.fightObjectModel = fightObjectModel;
            this.attackableView = attackableView;
            this.transform = transform;

            if (fightObjectModel.TypeAttack == TypeAttack.Melee)
            {
                attackableView.AttackHitEvent += MeleeAttack;
            }
        }

        public void Attack()
        {
            if (fightObjectModel.AimCharacter == null || fightObjectModel.AimCharacter.Equals(null))
                return;
            
            if (Time.time - lastAttackTime < fightObjectModel.AllAnimAttackTime)
                return;
            
            var distance = PositionExtention.GetDistanceBetweenObjects(transform, fightObjectModel.AimCharacter.Transform);
            if (distance > fightObjectModel.AttackRange)
            {
                attackableView.SetWalking(true);
                return;
            }
            
            lastAttackTime = Time.time;
            
            attackableView.SetAttack(true);
            
            if (fightObjectModel.TypeAttack == TypeAttack.Distance)
                DistanceAttack();
        }

        private void MeleeAttack()
        {
            if (fightObjectModel.AimCharacter == null)
            {
                attackableView.SetWalking(true);
                return;
            }

            var distance = PositionExtention.GetDistanceBetweenObjects(transform, fightObjectModel.AimCharacter.Transform);
            if (distance > fightObjectModel.AttackRange)
            {
                attackableView.SetWalking(true);
                return;
            }
            
            fightObjectModel.AimCharacter.CurrentHealth -= fightObjectModel.DamageAmount;
            if (fightObjectModel.AimCharacter.CurrentHealth <= 0)
            {
                if (fightObjectModel.AimCharacter.WarSide == WarSide.Enemy)
                {
                    GlobalObjects.GameData.allDamagables.Remove(fightObjectModel.AimCharacter);
                    Object.Destroy(fightObjectModel.AimCharacter.Transform.gameObject);
                }
                else
                {
                    fightObjectModel.AimCharacter.Transform.gameObject.SetActive(false);
                }
            }
        }
        
        private void DistanceAttack()
        {
            ShootProjectile();
        }
        
        private void ShootProjectile()
        {
            if (attackableView.ProjectilePrefab == null || attackableView.FirePoint == null || fightObjectModel.AimCharacter == null)
                return;

            GameObject projectile = Object.Instantiate(attackableView.ProjectilePrefab, attackableView.FirePoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null)
                return;

            var velocity = CalculateBallisticVelocityVector(attackableView.FirePoint.position, 
                fightObjectModel.AimCharacter.Transform.position, attackableView.ProjectileSpeed);
            if (velocity.HasValue)
            {
                rb.useGravity = true;
                rb.velocity = velocity.Value;

                var damageComponent = projectile.GetComponent<Projectile>();
                if (damageComponent != null)
                {
                    damageComponent.damage = fightObjectModel.DamageAmount;
                    damageComponent.ownerWarSide = fightObjectModel.WarSide;
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
