using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Concrete.ArcherEnemy;
using _Project.Scripts.GameObjects.Concrete.ArcherFriend;
using _Project.Scripts.GameObjects.Concrete.FlyingEnemy;
using _Project.Scripts.GameObjects.Concrete.FriendsBuild;
using _Project.Scripts.GameObjects.Concrete.MoneyBuild;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.GameObjects.Concrete.TowerDefence;
using _Project.Scripts.GameObjects.Concrete.WarriorEnemy;
using _Project.Scripts.GameObjects.Concrete.WarriorFriend;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;
using UnitModel = _Project.Scripts.GameObjects.Abstract.Unit.UnitModel;

namespace _Project.Scripts.GameObjects.Abstract.BaseObject
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(BuildModel))]
    [MemoryPackUnion(1, typeof(MoneyBuildModel))]
    [MemoryPackUnion(2, typeof(FriendsBuildModel))]
    [MemoryPackUnion(3, typeof(TowerDefenceModel))]
    [MemoryPackUnion(4, typeof(UnitModel))]
    [MemoryPackUnion(5, typeof(PlayerModel))]
    [MemoryPackUnion(6, typeof(WarriorEnemyModel))]
    [MemoryPackUnion(7, typeof(WarriorFriendModel))]
    [MemoryPackUnion(8, typeof(ArcherEnemyModel))]
    [MemoryPackUnion(9, typeof(ArcherFriendModel))]
    [MemoryPackUnion(10, typeof(FlyingEnemyModel))]
    public abstract partial class ObjectModel : IHealthModel, ISavableModel
    {
        [MemoryPackInclude] [field:SerializeField] public int Id { get; set; }
        
        [Header("Health")] 
        [SerializeField] private WarSide _warSide;
        [SerializeField] private float _delayRegeneration = 3f;
        [SerializeField] private float _regenerateHealthInSecond = 5f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth = 100f;
        
        public int SecondsWithoutDamage { get; set; }
        
        public WarSide WarSide
        {
            get => _warSide;
            set => _warSide = value;
        }

        public float DelayRegeneration
        {
            get => _delayRegeneration;
            set => _delayRegeneration = value;
        }

        public float RegenerateHealthInSecond
        {
            get => _regenerateHealthInSecond;
            set => _regenerateHealthInSecond = value;
        }

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        [MemoryPackInclude]
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (value < _currentHealth)
                    SecondsWithoutDamage = 0;
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            }
        }

        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
    }
}