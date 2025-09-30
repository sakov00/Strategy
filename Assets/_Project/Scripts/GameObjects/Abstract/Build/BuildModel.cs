using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.FriendsBuild;
using _Project.Scripts.GameObjects.Concrete.MoneyBuild;
using _Project.Scripts.GameObjects.Concrete.TowerDefence;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Abstract
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(MoneyBuildModel))]
    [MemoryPackUnion(1, typeof(FriendsBuildModel))]
    [MemoryPackUnion(2, typeof(TowerDefenceModel))]
    public abstract partial class BuildModel : ObjectModel, IUpgradableModel
    {
        [field: Header("Build Type")]
        [field: SerializeField]
        public BuildType BuildType { get; set; }

        [Header("Upgrades")] [SerializeField] private List<int> _priceList;

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