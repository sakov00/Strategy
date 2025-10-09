using System;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.Player
{
    [Serializable]
    [MemoryPackable]
    public partial class PlayerModel : UnitModel
    {
        [Header("Resurrection")] 
        [SerializeField] private int _needTimeNoDamage = 2;
        [SerializeField] private int _currentTimeNoDamage;
        [SerializeField] private bool _isNoDamageable;
        [SerializeField] private int _needTimeResurrection = 3;
        [SerializeField] private int _currentTimeResurrection;

        public int NeedTimeNoDamage => _needTimeNoDamage;

        public int CurrentTimeNoDamage
        {
            get => _currentTimeNoDamage;
            set => _currentTimeNoDamage = value;
        }
        public bool IsNoDamageable
        {
            get => _isNoDamageable;
            set => _isNoDamageable = value;
        }

        public int NeedTimeResurrection => _needTimeResurrection;
        public int CurrentTimeResurrection
        {
            get => _currentTimeResurrection;
            set => _currentTimeResurrection = value;
        }
        
        public override float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if(IsNoDamageable) return;
                if (value < _currentHealth)
                    SecondsWithoutDamage = 0;
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            }
        }
    }
}