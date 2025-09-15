using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.FriendsBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private int _timeCreateUnits = 5;
        
        public CharacterType CharacterType
        {
            get => _characterType;
            set => _characterType = value;
        }

        public int TimeCreateUnits
        {
            get => _timeCreateUnits;
            set => _timeCreateUnits = value;
        }
        
        public List<UnitModel> SaveBuildUnits { get; set; } = new();
        
        public int CurrentSpawnTimer { get; set; } = -1;
        
        [MemoryPackIgnore] public List<UnitController> BuildUnits { get; set; } = new();
        
        public Queue<UnitController> SpawnQueue { get; set; } = new();
    }
}