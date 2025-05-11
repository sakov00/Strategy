using System;
using _Project.Scripts.Enums;
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
        public Vector3? InputVector;
        public CharacterController characterController;
        
        [Header("Health")]
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        
        [Header("Attack")]
        public float attackRange = 2f;
        public int damageAmount = 10;
        public float delayAttack = 1f;
        public float detectionRadius = 10f;
        public WarSide warSide;
        public CharacterModel aimCharacter;

        private void OnValidate()
        {
            characterController ??= GetComponent<CharacterController>();
        }
    }
}