using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Projectiles;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [RequireComponent(typeof(IDamagable))]
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float projectileSpeed = 10f;
        
        private IFightObject characterModel;
        private float lastAttackTime = -Mathf.Infinity;

        private void OnValidate()
        {
            characterModel ??= GetComponent<IFightObject>();
        }

        public void Attack()
        {
            if (characterModel.AimCharacter == null || characterModel.AimCharacter.Equals(null))
                return;
            
            if (Time.time - lastAttackTime < characterModel.DelayAttack)
                return;

            if (characterModel.TypeAttack == TypeAttack.Melee)
                MeleeAttack();
            if (characterModel.TypeAttack == TypeAttack.Distance)
                DistanceAttack();
        }

        private void MeleeAttack()
        {
            var distance = Vector3.Distance(transform.position, characterModel.AimCharacter.Transform.position);
            if (distance <= characterModel.AttackRange)
            {
                lastAttackTime = Time.time;
                characterModel.AimCharacter.CurrentHealth -= characterModel.DamageAmount;
                if (characterModel.AimCharacter.CurrentHealth <= 0)
                    Destroy(characterModel.AimCharacter.Transform.gameObject);
            }
        }
        
        private void DistanceAttack()
        {
            var distance = Vector3.Distance(transform.position, characterModel.AimCharacter.Transform.position);
            if (distance <= characterModel.AttackRange)
            {
                lastAttackTime = Time.time;
                ShootProjectile();
            }
        }
        
        private void ShootProjectile()
        {
            if (projectilePrefab == null || firePoint == null || characterModel.AimCharacter == null)
                return;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null)
                return;

            var velocity = CalculateBallisticVelocityVector(firePoint.position, characterModel.AimCharacter.Transform.position, projectileSpeed);
            if (velocity.HasValue)
            {
                rb.useGravity = true;
                rb.velocity = velocity.Value;

                var damageComponent = projectile.GetComponent<Projectile>();
                if (damageComponent != null)
                {
                    damageComponent.damage = characterModel.DamageAmount;
                    damageComponent.ownerWarSide = characterModel.WarSide;
                }
            }
            else
            {
                Debug.LogWarning("Цель вне досягаемости с заданной скоростью!");
                Destroy(projectile); // не тратить лишние объекты
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
