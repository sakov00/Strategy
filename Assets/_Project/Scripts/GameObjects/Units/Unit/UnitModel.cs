using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.GameObjects.Units.Character;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Units.Unit
{
    [Serializable]
    public class UnitModel : CharacterModel
    {
        [Header("Unit Info")]
        public int _currentWaypointIndex;
        public List<Vector3> _wayToAim;
        public CharacterType _characterType;
        
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
        public CharacterType CharacterType
        {
            get => _characterType;
            set => _characterType = value;
        }
    }
}