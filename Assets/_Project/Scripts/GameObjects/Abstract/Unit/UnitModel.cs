using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Abstract.Unit
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(PlayerModel))]
    public abstract partial class UnitModel : ObjectModel, IFightObjectModel
    {
        [field: Header("Unit Type")]
        [field: SerializeField] public UnitType UnitType { get; set; }

        [Header("Movement")] 
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _gravity = -20f;

        [Header("Attack")] 
        [SerializeField] private float _attackRange = 10f;
        [SerializeField] private int _damageAmount = 10;
        [SerializeField] private float _allAnimAttackTime = 1f;
        [SerializeField] private float _animAttackTime = 1f;
        [SerializeField] private float _detectionRadius = 20f;
        [SerializeField] private TypeAttack _typeAttack = TypeAttack.Distance;
        [SerializeField] private ObjectController _aimObject;

        [Header("Way Info")] 
        [SerializeField] private int _currentWaypointIndex;
        [SerializeField] private List<Vector3> _wayToAim;

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        public float RotationSpeed
        {
            get => _rotationSpeed;
            set => _rotationSpeed = value;
        }

        public float Gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        public int CurrentWaypointIndex
        {
            get => _currentWaypointIndex;
            set => _currentWaypointIndex = value;
        }

        public List<Vector3> WayToAim
        {
            get => _wayToAim;
            set => _wayToAim = value;
        }

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