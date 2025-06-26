using UnityEngine;

namespace _Project.Scripts.GameObjects.Projectiles
{
    public class Arrow : Projectile
    {
        [Header("Flight Settings")]
        [SerializeField] private float launchAngle = 30f;
        [SerializeField] private float gravity = 9.81f;

        private float flightTime;
        private Vector3 startPosition;
        private Vector3 launchDirection;
        private float vx, vy;
        private bool isFlying;

        public override void LaunchToPoint(Vector3 targetPosition, float initialSpeed)
        {
            startPosition = transform.position;
            var toTarget = targetPosition - startPosition;

            var flat = new Vector3(toTarget.x, 0f, toTarget.z);
            var distance = flat.magnitude;
            var height = toTarget.y;

            var speedSq = initialSpeed * initialSpeed;
            var discriminant = speedSq * speedSq - gravity * (gravity * distance * distance + 2 * height * speedSq);

            if (discriminant < 0f)
            {
                Debug.LogWarning("Недостаточно скорости, чтобы достичь цели");
                return;
            }

            var sqrtDisc = Mathf.Sqrt(discriminant);

            var angleRad = Mathf.Atan((speedSq - sqrtDisc) / (gravity * distance));

            vx = initialSpeed * Mathf.Cos(angleRad);
            vy = initialSpeed * Mathf.Sin(angleRad);
            launchDirection = flat.normalized;

            flightTime = 0f;
            isFlying = true;
        }

        private void Update()
        {
            if (!isFlying) return;

            flightTime += Time.deltaTime;

            var x = vx * flightTime;
            var y = vy * flightTime - 0.5f * gravity * flightTime * flightTime;

            var newPosition = startPosition + launchDirection * x + Vector3.up * y;
            transform.position = newPosition;

            var velocity = launchDirection * vx + Vector3.up * (vy - gravity * flightTime);
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }
    }
}