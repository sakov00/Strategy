using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    [Serializable]
    public class BuildModel : ObjectModel, IUpgradableModel
    {
        [Header("Upgrades")] 
        [SerializeField] private int currentLevel = 1;

        public int CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }
    }
}