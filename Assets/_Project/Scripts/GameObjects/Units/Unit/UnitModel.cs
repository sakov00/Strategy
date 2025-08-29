using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.GameObjects.Characters.Unit
{
    [Serializable]
    public class UnitModel : CharacterModel
    {
        public int _currentWaypointIndex;
        public List<Vector3> _wayToAim;
        public UnitType _unitType;
        
        public int CurrentWaypointIndex
        {
            get => _currentWaypointIndex;
            set => _currentWaypointIndex = value;
        }
        public List<Vector3> WayToAim
        {
            get => _wayToAim;
            set => _wayToAim = value;
        }
        public UnitType UnitType
        {
            get => _unitType;
            set => _unitType = value;
        }
    }
}