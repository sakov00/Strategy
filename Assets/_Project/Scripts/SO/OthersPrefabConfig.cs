using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects._Object.BuildingZone;
using _Project.Scripts.GameObjects.EnemyRoads;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "OthersPrefabConfig", menuName = "SO/Others Prefab Config")]
    public class OthersPrefabConfig : ScriptableObject
    {
        public BuildingZone buildingZonePrefab;
    }
}