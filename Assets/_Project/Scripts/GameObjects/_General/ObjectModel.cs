using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Model;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    [Serializable]
    public class ObjectModel : IHealthModel
    {
        [Header("Health")] 
        [SerializeField] private WarSide _warSide;
        [SerializeField] private float _delayRegeneration = 3f;
        [SerializeField] private float _regenerateHealthInSecond = 5f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth = 100f;
        
        public Transform Transform { get; set; }
        public float HeightObject { get; set; }
        public Vector3 NoAimPos { get; set; }
        public WarSide WarSide => _warSide;
        public float DelayRegeneration => _delayRegeneration;
        public int SecondsWithoutDamage { get; set; }
        public float RegenerateHealthInSecond => _regenerateHealthInSecond;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth
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