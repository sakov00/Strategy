using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    public class PlayerMovementSystem
    {
        private readonly PlayerModel playerModel;
        private readonly PlayerView playerView;
        private readonly Transform transform;
        private Vector3 velocity;

        public PlayerMovementSystem(PlayerModel playerModel, PlayerView playerView, Transform transform)
        {
            this.playerModel = playerModel;
            this.playerView = playerView;
            this.transform = transform;
            
            playerView.Agent.updateRotation = false;
            playerView.Agent.acceleration = 100f;
            playerView.Agent.autoBraking = false;
        }

        public void MoveTo(Vector3 inputVector)
        {
            if (inputVector.sqrMagnitude < 0.01f)
            {
                playerView.Agent.isStopped = true;
                return;
            }

            Vector3 direction = Vector3.ClampMagnitude(inputVector, 1f);
            Vector3 destination = transform.position + direction * 2f;

            playerView.Agent.isStopped = false;
            playerView.Agent.speed = playerModel.MoveSpeed;
            playerView.Agent.SetDestination(destination);

            if (playerView.Agent.velocity.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(playerView.Agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerModel.RotationSpeed * Time.deltaTime * 3f);
            }
        }
    }
}
