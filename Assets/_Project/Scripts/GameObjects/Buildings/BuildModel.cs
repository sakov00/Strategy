using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    [Serializable]
    public abstract class BuildModel : ObjectModel, IUpgradableModel
    {
        [Header("Upgrades")] 
        [SerializeField] private List<int> priceList;
        [SerializeField] private int currentLevel;

        public int CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }
        
        public List<int> PriceList
        {
            get => priceList;
            set => priceList = value;
        }
    }
}