using _Project.Scripts.Models;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    [RequireComponent(typeof(CharacterModel))]
    public class MovementSystem : MonoBehaviour
    {
        private CharacterModel characterModel;
        private Vector3 velocity;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
        }

        public void MoveTo(Vector3 inputVector)
        {
            if (characterModel.characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            var move = Vector3.ClampMagnitude(inputVector, 1f);
            
            if (move.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, characterModel.rotationSpeed * Time.deltaTime);
            }

            characterModel.characterController.Move(move * characterModel.moveSpeed * Time.deltaTime);

            velocity.y += characterModel.gravity * Time.deltaTime;
            characterModel.characterController.Move(velocity * Time.deltaTime);
        }
    }
}