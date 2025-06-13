using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    [Serializable]
    public class BuildModel : IHealthModel
    {
        [Header("Render")]
        [SerializeField] public Renderer objRenderer;

        [Header("Upgrades")] 
        public int currentLevel;

        [Header("Health")] 
        public WarSide warSide;
        public float maxHealth = 500f;
        public float currentHealth = 500f;
        
        public Transform Transform { get; set; }
        public WarSide WarSide => warSide;
        public float MaxHealth => maxHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }
}