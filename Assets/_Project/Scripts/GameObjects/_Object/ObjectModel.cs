using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.GameObjects._Object.FriendsBuild;
using _Project.Scripts.GameObjects._Object.MoneyBuild;
using _Project.Scripts.GameObjects._Object.TowerDefence;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(BuildModel))]
    [MemoryPackUnion(1, typeof(MoneyBuildModel))]
    [MemoryPackUnion(2, typeof(FriendsBuildModel))]
    [MemoryPackUnion(3, typeof(TowerDefenceModel))]
    [MemoryPackUnion(4, typeof(CharacterModel))]
    [MemoryPackUnion(5, typeof(PlayerModel))]
    [MemoryPackUnion(6, typeof(UnitModel))]
    public abstract partial class ObjectModel : IHealthModel, ISavableModel
    {
        [Header("Health")] 
        [SerializeField] private WarSide _warSide;
        [SerializeField] private float _delayRegeneration = 3f;
        [SerializeField] private float _regenerateHealthInSecond = 5f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth = 100f;
        
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        
        public float HeightObject { get; set; }
        public Vector3 NoAimPos { get; set; }
        
        public int SecondsWithoutDamage { get; set; }
        public WarSide WarSide => _warSide;
        public float DelayRegeneration => _delayRegeneration;
        public float RegenerateHealthInSecond => _regenerateHealthInSecond;
        public float MaxHealth => _maxHealth;
        [MemoryPackInclude] public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (value < _currentHealth)
                    SecondsWithoutDamage = 0;
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            }
        }
        
    }
}