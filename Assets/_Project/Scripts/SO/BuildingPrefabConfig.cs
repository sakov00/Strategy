using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabConfig", menuName = "SO/Building Prefab Config")]
    public class BuildingPrefabConfig : ScriptableObject
    {
        public BuildModel moneyBuildingPrefab;
        public BuildModel meleeFriendBuildingPrefab;
        public BuildModel distanceFriendBuildingPrefab;
        public BuildModel towerBuildingPrefab;
        public BuildModel mainBuildingPrefab;
    }
}