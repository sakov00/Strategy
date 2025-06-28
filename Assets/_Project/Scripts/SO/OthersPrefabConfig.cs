using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.SpawnPoints;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "OthersPrefabConfig", menuName = "SO/Others Prefab Config")]
    public class OthersPrefabConfig : ScriptableObject
    {
        public SpawnPoint spawnPointPrefab;
        public BuildingZoneController buildingZonePrefab;
    }
}