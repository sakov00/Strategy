using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Units.Unit;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [Serializable]
    public class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [SerializeField] public CharacterType _characterType;
        [SerializeField] public List<UnitController> _buildUnits = new();
        
        public CharacterType CharacterType
        {
            get => _characterType;
            set => _characterType = value;
        }
        
        public List<UnitController> BuildUnits
        {
            get => _buildUnits;
            set => _buildUnits = value;
        }
    }
}