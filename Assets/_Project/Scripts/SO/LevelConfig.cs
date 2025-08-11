using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.Environment;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "SO/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] public List<TerrainController> terrainControllers = new();
        [SerializeField] public List<EnvironmentController> environmentPrefabs = new();
        [SerializeField] public List<LevelIndex> roadPrefabs = new();
        
        [Serializable]
        public class LevelIndex
        {
            public List<EnemyRoad> roads;
        }
    }
}