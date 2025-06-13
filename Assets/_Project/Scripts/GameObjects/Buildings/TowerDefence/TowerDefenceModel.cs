using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.TowerDefence
{
    [Serializable]
    public class TowerDefenceModel : BuildModel, IFightObjectModel
    {
        [Header("Attack")] 
        [SerializeField] private float attackRange = 20f;
        [SerializeField] private int damageAmount = 10;
        [SerializeField] private float allAnimAttackTime = 1f;
        [SerializeField] private float animAttackTime = 1f;
        [SerializeField] private float detectionRadius = 20f;
        [SerializeField] private TypeAttack typeAttack = TypeAttack.Distance;
        [SerializeField] private IHealthModel aimCharacter;

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