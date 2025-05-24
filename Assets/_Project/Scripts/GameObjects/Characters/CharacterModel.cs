using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters
{
    [Serializable]
    public class CharacterModel : MonoBehaviour, IFightObject
    {
        [Header("Movement")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 10f; 
        public float gravity = -20f;

        [field: Header("Health")] 
        [field: SerializeField] public WarSide WarSide { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; } = 100f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 100f;
        public Transform Transform => transform;
        
        [field: Header("Attack")] 
        [field: SerializeField] public float AttackRange { get; set; } = 10f;
        [field: SerializeField] public int DamageAmount { get; set; } = 10;
        [field: SerializeField] public float DelayAttack { get; set; } = 1f;
        [field: SerializeField] public float DetectionRadius { get; set; } = 20f;
        [field: SerializeField] public TypeAttack TypeAttack { get; set; } = TypeAttack.Distance;
        [field: SerializeField] public IDamagable AimCharacter { get; set; }
    }
}