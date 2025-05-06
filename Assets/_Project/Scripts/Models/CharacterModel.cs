using System;
using UnityEngine;

namespace _Project.Scripts.Models
{
    [Serializable]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterModel : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 10f; 
        public float gravity = -20f;
        public CharacterController characterController;
        
        [Header("Health")]
        public float maxHealth = 100f;
        public float currentHealth = 100f;

        private void OnValidate()
        {
            characterController ??= GetComponent<CharacterController>();
        }
    }
}