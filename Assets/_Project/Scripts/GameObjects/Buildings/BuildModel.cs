using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    [Serializable]
    public class BuildModel
    {
        [Header("Render")]
        [SerializeField] public Renderer objRenderer;

        [Header("Upgrades")] 
        public int currentLevel;

        [Header("Health")] 
        public WarSide warSide;
        public float maxHealth = 500f;
        public float currentHealth = 500f;
    }
}