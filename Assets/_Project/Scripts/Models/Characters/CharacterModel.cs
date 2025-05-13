using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Models.Characters
{
    [Serializable]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterModel : MonoBehaviour, IDamagable
    {
        [Header("Movement")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 10f; 
        public float gravity = -20f;
        public Vector3? InputVector;
        public CharacterController characterController;

        [field: Header("Health")] 
        [field: SerializeField] public WarSide WarSide { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; } = 100f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 100f;
        public Transform Transform => transform;
        
        [Header("Attack")]
        public float attackRange = 2f;
        public int damageAmount = 10;
        public float delayAttack = 1f;
        public float detectionRadius = 10f;
        public TypeAttack typeAttack;
        public IDamagable aimCharacter;

        private void OnValidate()
        {
            characterController ??= GetComponent<CharacterController>();
        }
    }
}