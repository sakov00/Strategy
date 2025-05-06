using System;
using _Project.Scripts.Models;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace _Project.Scripts
{
    [RequireComponent(typeof(CharacterModel))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterModel characterModel;
        
        private Vector3 velocity;

        private void OnValidate()
        {
            characterModel ??= GetComponent<CharacterModel>();
        }

        public void Move(Vector3 input)
        {
            if (characterModel.characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            var move = Vector3.ClampMagnitude(input, 1f);
            
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