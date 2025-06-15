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
        [SerializeField] private WarSide warSide;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        
        public Transform Transform { get; set; }
        public Vector3 NoAimPos { get; set; }
        public WarSide WarSide => warSide;
        public float MaxHealth => maxHealth;

        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }
}