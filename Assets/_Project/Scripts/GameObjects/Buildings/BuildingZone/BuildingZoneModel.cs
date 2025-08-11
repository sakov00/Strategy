using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    [Serializable]
    public class BuildingZoneModel
    {
        [SerializeField] public TypeBuilding _typeBuilding;
        
        public TypeBuilding TypeBuilding
        {
            get => _typeBuilding;
            set => _typeBuilding = value;
        }
    }
}