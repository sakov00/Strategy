using System;
using System.Collections.Generic;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.LevelEnvironment.Environment;
using _Project.Scripts.GameObjects.LevelEnvironment.Terrain;
using UnityEngine;

namespace _General.Scripts.SO
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "SO/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] public List<TerrainController> terrainControllers = new();
        [SerializeField] public List<EnvironmentController> environmentPrefabs = new();
        [SerializeField] public EnemyRoadController roadPrefab = new();
    }
}