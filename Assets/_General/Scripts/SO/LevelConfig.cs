using System.Collections.Generic;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Environment;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain;
using UnityEngine;

namespace _General.Scripts.SO
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "SO/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] public List<TerrainController> terrainControllers;
        [SerializeField] public List<EnvironmentController> environmentPrefabs;
        [SerializeField] public EnemyRoadController roadPrefab;
    }
}