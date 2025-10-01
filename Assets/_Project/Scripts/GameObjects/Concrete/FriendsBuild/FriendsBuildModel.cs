using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.FriendsBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [SerializeField] private UnitType _unitType;
        [SerializeField] private int _timeCreateUnits = 5;
        [SerializeField] private List<int> _buildUnitIds = new();

        public UnitType UnitType
        {
            get => _unitType;
            set => _unitType = value;
        }

        public int TimeCreateUnits
        {
            get => _timeCreateUnits;
            set => _timeCreateUnits = value;
        }
        
        public List<int> BuildUnitIds
        {
            get => _buildUnitIds;
            set => _buildUnitIds = value;
        }
        
        public int NeedRestoreUnitsCount { get; set; }
        public int CurrentSpawnTimer { get; set; } = -1;
    }
}