using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using UnityEngine;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabConfig", menuName = "SO/Building Prefab Config")]
    public class BuildingPrefabConfig : ScriptableObject
    {
        public BuildingZoneController buildingZonePrefab;
        public MoneyBuildController moneyBuildingPrefab;
        public FriendsBuildController meleeFriendBuildingPrefab;
        public FriendsBuildController distanceFriendBuildingPrefab;
        public TowerDefenceController towerBuildingPrefab;
        public BuildController mainBuildingPrefab;
    }
}