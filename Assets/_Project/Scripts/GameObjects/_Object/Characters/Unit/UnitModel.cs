using System;
using System.Collections.Generic;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.Characters.Unit
{
    [Serializable]
    [MemoryPackable]
    public partial class UnitModel : CharacterModel
    {
        [Header("Unit Info")]
        [SerializeField] private int _currentWaypointIndex;
        [SerializeField] private List<Vector3> _wayToAim;
        
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
    }
}