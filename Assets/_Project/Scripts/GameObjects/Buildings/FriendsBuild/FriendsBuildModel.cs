using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [Serializable]
    public class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [SerializeField] public UnitType _unitType;
        [SerializeField] public List<UnitController> _buildUnits = new();
        
        public UnitType UnitType
        {
            get => _unitType;
            set => _unitType = value;
        }
        
        public List<UnitController> BuildUnits
        {
            get => _buildUnits;
            set => _buildUnits = value;
        }
    }
}