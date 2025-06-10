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
        }

        public void MoveTo(Vector3 inputVector)
        {
            if (playerView.CharacterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            var move = Vector3.ClampMagnitude(inputVector, 1f);
            
            if (move.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerModel.rotationSpeed * Time.deltaTime);
            }
            
            playerView.CharacterController.Move(move * playerModel.moveSpeed * Time.deltaTime);

            velocity.y += playerModel.gravity * Time.deltaTime;
            playerView.CharacterController.Move(velocity * Time.deltaTime);
        }
    }
}