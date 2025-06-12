using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    public class CharacterSimpleController : MonoBehaviour, IUpgradable, IDamagable
    {
        protected CharacterModel CharacterModel;
        protected CharacterView CharacterView;
        
        public int CurrentLevel { get; set; }
        
        public Transform Transform => transform;
        public WarSide WarSide => CharacterModel.warSide;
        public float MaxHealth => CharacterModel.maxHealth;
        public float CurrentHealth
        {
            get => CharacterModel.currentHealth;
            set => CharacterModel.currentHealth = value;
        }

        private void Awake()
        {
            CharacterView.SetWalking(true);
        }
    }
}