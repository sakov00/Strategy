using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    public class BuildModel : MonoBehaviour, IDamagable, IUpgradable
    {
        [Header("Render")]
        [SerializeField] public Renderer objRenderer;
        
        [field: Header("Upgrades")] 
        [field: SerializeField]public int CurrentLevel { get; set; }
        
        [field: Header("Health")] 
        [field: SerializeField] public WarSide WarSide { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; } = 500f;
        [field: SerializeField] public float CurrentHealth { get; set; } = 500f;
        public Transform Transform => transform;
    }
}