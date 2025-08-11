using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    [Serializable]
    public class ObjectModel : IHealthModel
    {
        [Header("Health")] 
        [SerializeField] private WarSide _warSide;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth = 100f;
        
        public Transform Transform { get; set; }
        public float HeightObject { get; set; }
        public Vector3 NoAimPos { get; set; }
        public WarSide WarSide => _warSide;
        public float MaxHealth => _maxHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
        }
    }
}