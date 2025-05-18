using _Project.Scripts.GameObjects.Characters.Player;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [RequireComponent(typeof(PlayerModel))]
    public class PlayerMovementSystem : MonoBehaviour
    {
        private PlayerModel playerModel;
        private Vector3 velocity;

        private void OnValidate()
        {
            playerModel ??= GetComponent<PlayerModel>();
        }

        public void MoveTo(Vector3 inputVector)
        {
            if (playerModel.characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            var move = Vector3.ClampMagnitude(inputVector, 1f);
            
            if (move.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerModel.rotationSpeed * Time.deltaTime);
            }
            
            playerModel.characterController.Move(move * playerModel.moveSpeed * Time.deltaTime);

            velocity.y += playerModel.gravity * Time.deltaTime;
            playerModel.characterController.Move(velocity * Time.deltaTime);
        }
    }
}