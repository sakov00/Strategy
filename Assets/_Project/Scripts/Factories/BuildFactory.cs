using _Project.Scripts._GlobalLogic;
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
        
        public BuildController CreateBuild(TypeBuilding building, Vector3 position, Quaternion rotation)
        {
            BuildController model;
            switch (building)
            {
                case TypeBuilding.MoneyBuilding:
                    model = CreateMoneyBuilding(position, rotation);
                    break;
                case TypeBuilding.TowerDefenseBuilding:
                    model = CreateTowerDefenseBuilding(position, rotation);
                    break;
                case TypeBuilding.FriendMeleeBuilding:
                    model = CreateMeleeFriendBuilding(position, rotation);
                    break;
                case TypeBuilding.FriendDistanceBuilding:
                    model = CreateDistanceFriendBuilding(position, rotation);
                    break;
                default: return null;
            }
            
            GlobalObjects.GameData.allDamagables.Add(model);
            return model;
        }

        private BuildController CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.moneyBuildingPrefab, position, rotation);
        }
        
        private BuildController CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.towerBuildingPrefab, position, rotation);
        }
        
        private BuildController CreateMeleeFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.meleeFriendBuildingPrefab, position, rotation);;
        }
        
        private BuildController CreateDistanceFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(buildingPrefabConfig.distanceFriendBuildingPrefab, position, rotation);
        }
    }
}