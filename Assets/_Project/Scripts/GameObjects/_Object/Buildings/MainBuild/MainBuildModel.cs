using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.MainBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class MainBuildModel : BuildModel, IFightObjectModel
    {
        [Header("Attack")] 
        [SerializeField] private float _attackRange = 20f;
        [SerializeField] private int _damageAmount = 10;
        [SerializeField] private float _allAnimAttackTime = 1f;
        [SerializeField] private float _animAttackTime = 1f;
        [SerializeField] private float _detectionRadius = 20f;
        [SerializeField] private TypeAttack _typeAttack = TypeAttack.Distance;
        [SerializeField] private ObjectController _aimObject;

        public float AttackRange
        {
            get => _attackRange;
            set => _attackRange = value;
        }

        public int DamageAmount
        {
            get => _damageAmount;
            set => _damageAmount = value;
        }
        
        public float AllAnimAttackTime
        {
            get => _allAnimAttackTime;
            set => _allAnimAttackTime = value;
        }

        public float AnimAttackTime
        {
            get => _animAttackTime;
            set => _animAttackTime = value;
        }

        public float DetectionRadius
        {
            get => _detectionRadius;
            set => _detectionRadius = value;
        }

        public TypeAttack TypeAttack
        {
            get => _typeAttack;
            set => _typeAttack = value;
        }

        [MemoryPackIgnore]
        public ObjectController AimObject
        {
            get => _aimObject;
            set => _aimObject = value;
        }
    }
}