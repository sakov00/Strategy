using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabConfig", menuName = "SO/Building Prefab Config")]
    public class BuildingPrefabConfig : ScriptableObject
    {
        public BuildController moneyBuildingPrefab;
        public BuildController meleeFriendBuildingPrefab;
        public BuildController distanceFriendBuildingPrefab;
        public BuildController towerBuildingPrefab;
        public BuildController mainBuildingPrefab;
    }
}