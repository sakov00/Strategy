using System;
using System.Collections.Generic;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.EnemyRoads
{
    [Serializable]
    [MemoryPackable]
    public partial class EnemyRoadModel : ISavableModel
    {
        [field:SerializeField] public List<EnemyGroup> RoundEnemyList { get; set; } = new();
        
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        
        public List<Vector3> WorldPositions { get; set; } = new();
        public int CurrentIndex { get; set; }
        public float ElapsedTime { get; set; }

    }
    
    [Serializable]
    [MemoryPackable]
    public partial class EnemyGroup
    {
        public List<EnemyWithTime> enemies;
    }

    [Serializable]
    [MemoryPackable]
    public partial class EnemyWithTime
    {
        [Min(0f)]
        public float time;
        public CharacterType enemyType;
    }
}