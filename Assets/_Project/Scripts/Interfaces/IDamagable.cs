using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IDamagable
    {
        public WarSide WarSide { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public Transform Transform { get; }
    }
}