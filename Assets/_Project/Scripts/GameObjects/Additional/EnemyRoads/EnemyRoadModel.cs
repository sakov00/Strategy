using System;
using System.Collections.Generic;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Additional.EnemyRoads
{
    [Serializable]
    [MemoryPackable]
    public partial class EnemyRoadModel : ISavableModel
    {
        [field: SerializeField] public List<EnemyGroup> RoundEnemyList { get; set; } = new();

        public SplineContainerData SplineContainerData { get; set; } = new();
        public List<Vector3> WorldPositions { get; set; } = new();
        public int CurrentIndex { get; set; }
        public float ElapsedTime { get; set; }

        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
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
        [Min(0f)] public float time;

        public UnitType enemyType;
    }
}