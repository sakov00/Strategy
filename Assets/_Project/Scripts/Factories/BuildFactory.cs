using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Interfaces;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class BuildFactory
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private BuildingPrefabConfig buildingPrefabConfig;
        
        public BuildModel CreateBuild(TypeBuilding building, Vector3 position, Quaternion rotation)
        {
            switch (building)
            {
                case TypeBuilding.MoneyBuilding:
                    return CreateMoneyBuilding(position, rotation);
                case TypeBuilding.TowerDefenseBuilding:
                    return CreateTowerDefenseBuilding(position, rotation);
                case TypeBuilding.FriendMeleeBuilding:
                    return CreateMeleeFriendBuilding(position, rotation);
                case TypeBuilding.FriendDistanceBuilding:
                    return CreateDistanceFriendBuilding(position, rotation);
                default: return null;
            }
        }

        private BuildModel CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.moneyBuildingPrefab, position, rotation);
        }
        
        private BuildModel CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.towerBuildingPrefab, position, rotation);
        }
        
        private BuildModel CreateMeleeFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.meleeFriendBuildingPrefab, position, rotation);;
        }
        
        private BuildModel CreateDistanceFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.distanceFriendBuildingPrefab, position, rotation);
        }
    }
}