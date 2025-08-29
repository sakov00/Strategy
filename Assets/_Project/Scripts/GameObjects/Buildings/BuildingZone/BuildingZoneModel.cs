using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    [Serializable]
    public class BuildingZoneModel
    {
        [SerializeField] public BuildType _buildType;
        
        public BuildType BuildType
        {
            get => _buildType;
            set => _buildType = value;
        }
    }
}