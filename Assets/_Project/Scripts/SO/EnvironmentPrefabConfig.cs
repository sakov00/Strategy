using System.Collections.Generic;
using _Project.Scripts.GameObjects.Environment;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "EnvironmentPrefabConfig", menuName = "SO/Environment Prefab Config")]
    public class EnvironmentPrefabConfig : ScriptableObject
    {
        [SerializeField] public List<TerrainController> terrainControllers = new();
        [SerializeField] public List<EnvironmentController> environmentPrefabs = new();
    }
}