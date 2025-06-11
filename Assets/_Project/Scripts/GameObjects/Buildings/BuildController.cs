using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    public class BuildController : MonoBehaviour, IUpgradable, IDamagable
    {
        protected BuildModel BuildModel;
        protected BuildView BuildView;
        
        public int CurrentLevel { get; set; }
        
        public Transform Transform => transform;
        public WarSide WarSide => BuildModel.warSide;
        public float MaxHealth => BuildModel.maxHealth;
        public float CurrentHealth
        {
            get => BuildModel.currentHealth;
            set => BuildModel.currentHealth = value;
        }
    }
}