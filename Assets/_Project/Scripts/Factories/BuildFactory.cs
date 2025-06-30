using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class BuildFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private BuildingPrefabConfig _buildingPrefabConfig;
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SaveRegistry _saveRegistry;

        public BuildModel GetBuildModel(TypeBuilding building)
        {
            BuildController buildController;
            switch (building)
            {
                case TypeBuilding.MoneyBuilding:
                    buildController = _buildingPrefabConfig.moneyBuildingPrefab;
                    break;
                case TypeBuilding.TowerDefenseBuilding:
                    buildController = _buildingPrefabConfig.towerBuildingPrefab;
                    break;
                case TypeBuilding.FriendMeleeBuilding:
                    buildController = _buildingPrefabConfig.meleeFriendBuildingPrefab;
                    break;
                case TypeBuilding.FriendDistanceBuilding:
                    buildController = _buildingPrefabConfig.distanceFriendBuildingPrefab;
                    break;
                default: return null;
            }
            return buildController.BuildModel;
        }
        
        public BuildController CreateBuild(TypeBuilding building, Vector3 position, Quaternion rotation)
        {
            BuildController buildController;
            switch (building)
            {
                case TypeBuilding.MoneyBuilding:
                    buildController = CreateMoneyBuilding(position, rotation);
                    break;
                case TypeBuilding.TowerDefenseBuilding:
                    buildController = CreateTowerDefenseBuilding(position, rotation);
                    break;
                case TypeBuilding.FriendMeleeBuilding:
                    buildController = CreateMeleeFriendBuilding(position, rotation);
                    break;
                case TypeBuilding.FriendDistanceBuilding:
                    buildController = CreateDistanceFriendBuilding(position, rotation);
                    break;
                default: return null;
            }
            
            _healthRegistry.Register(buildController.ObjectModel);
            return buildController;
        }
        
#region JSON Create List
        
        public List<MoneyBuildController> CreateMoneyBuildings(IEnumerable<MoneyBuildJson> moneyBuildJsons)
            => moneyBuildJsons.Select(CreateMoneyBuilding).ToList();
        
        public List<TowerDefenceController> CreateTowerDefenseBuildings(IEnumerable<TowerDefenceBuildJson> towerDefenceBuildJsons) 
            => towerDefenceBuildJsons.Select(CreateTowerDefenseBuilding).ToList();
        
        public List<FriendsBuildController> CreateMeleeFriendBuildings(IEnumerable<FriendsBuildJson> friendsBuildJsons)
            => friendsBuildJsons.Select(CreateMeleeFriendBuilding).ToList();
        
        public List<FriendsBuildController> CreateDistanceFriendBuildings(IEnumerable<FriendsBuildJson> friendsBuildJsons)
            => friendsBuildJsons.Select(CreateDistanceFriendBuilding).ToList();
        
#endregion

#region JSON Create One
        
        public MoneyBuildController CreateMoneyBuilding(MoneyBuildJson moneyBuildJson)
        {
            var moneyBuild = CreateMoneyBuilding();
            moneyBuild.SetJsonData(moneyBuildJson);
            return moneyBuild;
        }
        
        public TowerDefenceController CreateTowerDefenseBuilding(TowerDefenceBuildJson towerDefenceBuildJson)
        {
            var towerDefence = CreateTowerDefenseBuilding();
            towerDefence.SetJsonData(towerDefenceBuildJson);
            return towerDefence;
        }
        
        public FriendsBuildController CreateMeleeFriendBuilding(FriendsBuildJson friendsBuildJson)
        {
            var meleeFriendsBuild = CreateMeleeFriendBuilding();
            meleeFriendsBuild.SetJsonData(friendsBuildJson);
            return meleeFriendsBuild;
        }
        
        public FriendsBuildController CreateDistanceFriendBuilding(FriendsBuildJson friendsBuildJson)
        {
            var distanceFriendBuild = CreateDistanceFriendBuilding();
            distanceFriendBuild.SetJsonData(friendsBuildJson);
            return distanceFriendBuild;
        }
        
#endregion

#region Just Create One

        private MoneyBuildController CreateMoneyBuilding(Vector3 position = default, Quaternion rotation = default)
        {
            var moneyBuild = _resolver.Instantiate(_buildingPrefabConfig.moneyBuildingPrefab, position, rotation);
            return moneyBuild;
        }
        
        private TowerDefenceController CreateTowerDefenseBuilding(Vector3 position = default, Quaternion rotation = default)
        {
            var towerDefence = _resolver.Instantiate(_buildingPrefabConfig.towerBuildingPrefab, position, rotation);
            return towerDefence;
        }
        
        private FriendsBuildController CreateMeleeFriendBuilding(Vector3 position = default, Quaternion rotation = default)
        {
            var meleeFriendsBuild = _resolver.Instantiate(_buildingPrefabConfig.meleeFriendBuildingPrefab, position, rotation);
            return meleeFriendsBuild;
        }
        
        private FriendsBuildController CreateDistanceFriendBuilding(Vector3 position = default, Quaternion rotation = default)
        {
            var distanceFriendBuild = _resolver.Instantiate(_buildingPrefabConfig.distanceFriendBuildingPrefab, position, rotation);
            return distanceFriendBuild;
        }
        
#endregion
    }
}