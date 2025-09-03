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
        [SerializeField] public CharacterType _characterType;
        [SerializeField] public List<UnitModel> _buildUnits = new();
        
        public CharacterType CharacterType
        {
            get => _characterType;
            set => _characterType = value;
        }
        
        public List<UnitModel> BuildUnits
        {
            get => _buildUnits;
            set => _buildUnits = value;
        }
    }
}