using System;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IDamagable : IPositioned
    {
        public WarSide WarSide { get; }
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
    }
}