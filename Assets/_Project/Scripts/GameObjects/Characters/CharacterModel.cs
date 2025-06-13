using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [Serializable]
    public class CharacterModel : ObjectModel, IMovementModel, IFightObjectModel
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 10f; 
        [SerializeField] private float gravity = -20f;
        
        [Header("Attack")] 
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private int damageAmount = 10;
        [SerializeField] private float allAnimAttackTime = 1f;
        [SerializeField] private float animAttackTime = 1f;
        [SerializeField] private float detectionRadius = 20f;
        [SerializeField] private TypeAttack typeAttack = TypeAttack.Distance;
        [NonSerialized] private IHealthModel aimCharacter;
        
        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
        public float Gravity
        {
            get => gravity;
            set => gravity = value;
        }

        public float AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        public int DamageAmount
        {
            get => damageAmount;
            set => damageAmount = value;
        }   
        
        public float AllAnimAttackTime
        {
            get => allAnimAttackTime;
            set => allAnimAttackTime = value;
        }

        public float AnimAttackTime
        {
            get => animAttackTime;
            set => animAttackTime = value;
        }
        
        public float DetectionRadius
        {
            get => detectionRadius;
            set => detectionRadius = value;
        }

        public TypeAttack TypeAttack
        {
            get => typeAttack;
            set => typeAttack = value;
        }

        public IHealthModel AimCharacter
        {
            get => aimCharacter;
            set => aimCharacter = value;
        }
    }
}