using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Concrete.FriendsGroup;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.FriendsBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [field:SerializeField] private UnitType _unitType;
        [field:SerializeField] private int _timeCreateUnits = 5;
        [field:SerializeField] private int _friendsGroupId;

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
        
        public int FriendsGroupId
        {
            get => _friendsGroupId;
            set => _friendsGroupId = value;
        }
        
        public int NeedRestoreUnitsCount { get; set; }
        public int CurrentSpawnTimer { get; set; } = -1;
    }
}