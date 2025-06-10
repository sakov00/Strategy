using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [Serializable]
    public class CharacterModel
    {
        [Header("Movement")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 10f; 
        public float gravity = -20f;

        [Header("Health")] 
        public WarSide warSide;
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        
        [Header("Attack")] 
        public float attackRange = 10f;
        public int damageAmount = 10;
        public float delayAttack = 1f;
        public float detectionRadius = 20f;
        public TypeAttack typeAttack = TypeAttack.Distance;
        [NonSerialized] public IDamagable AimCharacter;
    }
}