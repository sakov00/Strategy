using _Project.Scripts.GameObjects.Concrete.BuildingZone;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "OthersPrefabConfig", menuName = "SO/Others Prefab Config")]
    public class OthersPrefabConfig : ScriptableObject
    {
        public BuildingZone buildingZonePrefab;
    }
}