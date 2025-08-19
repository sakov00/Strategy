using System;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IHealthModel : IPositionedModel
    {
        public WarSide WarSide { get; }
        public float DelayRegeneration { get; }
        public float RegenerateHealthInSecond { get; }
        public int SecondsWithoutDamage { get; set; }
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
    }
}