using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Model;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    [Serializable]
    public abstract class BuildModel : ObjectModel, IUpgradableModel
    {
        [Header("Upgrades")] 
        [SerializeField] private List<int> _priceList;
        [SerializeField] private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }
        
        public List<int> PriceList
        {
            get => _priceList;
            set => _priceList = value;
        }
    }
}