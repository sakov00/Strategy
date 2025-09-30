using System.Collections.Generic;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Concrete.BuildingZone;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabConfig", menuName = "SO/Building Prefab Config")]
    public class BuildingPrefabConfig : ScriptableObject
    {
        public BuildingZone buildZonePrefab;
        public List<BuildController> allBuildPrefabs;
    }
}