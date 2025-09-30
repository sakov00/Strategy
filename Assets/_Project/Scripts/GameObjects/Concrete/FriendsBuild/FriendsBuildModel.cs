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

        public int NeedRestoreUnitsCount { get; set; }
        public int CurrentSpawnTimer { get; set; } = -1;

        [MemoryPackIgnore] public List<UnitController> BuildUnits { get; set; } = new();

    }
}