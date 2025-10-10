using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.Player
{
    [Serializable]
    [MemoryPackable]
    public partial class PlayerModel : UnitModel
    {
        [field: Header("Resurrection")] 
        [MemoryPackIgnore][field:SerializeField] public int NeedTimeNoDamage { get; set; } = 2;
        [MemoryPackInclude][field:SerializeField] public int CurrentTimeNoDamage { get; set; }
        [MemoryPackInclude][field:SerializeField] public bool IsNoDamageable { get; set; }
        [MemoryPackIgnore][field:SerializeField] public int NeedTimeResurrection  { get; set; } = 3;
        [MemoryPackInclude][field:SerializeField] public int CurrentTimeResurrection { get; set; }
        
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
        
        public override void LoadFrom(ISavableModel model)
        {
            base.LoadFrom(model);
            if (model is not PlayerModel objectModel) return;
            CurrentTimeNoDamage = objectModel.CurrentTimeNoDamage;
            IsNoDamageable = objectModel.IsNoDamageable;
            CurrentTimeResurrection = objectModel.CurrentTimeResurrection;
        }
    }
}