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
        
        public override void LoadData(ISavableModel model)
        {
            base.LoadData(model);
            if (model is not PlayerModel objectModel) return;
            CurrentTimeNoDamage = objectModel.CurrentTimeNoDamage;
            IsNoDamageable = objectModel.IsNoDamageable;
            CurrentTimeResurrection = objectModel.CurrentTimeResurrection;
        }
        
        public override ISavableModel GetSaveData()
        {
            var model = new PlayerModel
            {
                CurrentTimeNoDamage = CurrentTimeNoDamage,
                IsNoDamageable = IsNoDamageable,
                CurrentTimeResurrection = CurrentTimeResurrection,
            };
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}