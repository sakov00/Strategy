using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
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
        [Inject] private BuildRegistry _buildRegistry;

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
        
        public List<MoneyBuildController> CreateMoneyBuilding(IEnumerable<MoneyBuildJson> moneyBuildJsons)
        {
            var moneyBuildControllers = new List<MoneyBuildController>();
            foreach (var moneyBuildJson in moneyBuildJsons)
            {
                var moneyBuild = _resolver.Instantiate(_buildingPrefabConfig.moneyBuildingPrefab);
                moneyBuild.SetJsonData(moneyBuildJson);
                _buildRegistry.Register(moneyBuild);
                moneyBuildControllers.Add(moneyBuild);
            }
            return moneyBuildControllers;
        }
        
        public List<TowerDefenceController> CreateTowerDefenseBuilding(IEnumerable<TowerDefenceBuildJson> towerDefenceBuildJsons)
        {
            var towerDefenceControllers = new List<TowerDefenceController>();
            foreach (var towerDefenceBuildJson in towerDefenceBuildJsons)
            {
                var towerDefence = _resolver.Instantiate(_buildingPrefabConfig.towerBuildingPrefab);
                towerDefence.SetJsonData(towerDefenceBuildJson);
                _buildRegistry.Register(towerDefence);
                towerDefenceControllers.Add(towerDefence);
            }
            return towerDefenceControllers;
        }
        
        public List<FriendsBuildController> CreateMeleeFriendBuilding(IEnumerable<FriendsBuildJson> friendsBuildJsons)
        {
            var friendsBuildControllers = new List<FriendsBuildController>();
            foreach (var friendsBuildJson in friendsBuildJsons)
            {
                var meleeFriendsBuild = _resolver.Instantiate(_buildingPrefabConfig.meleeFriendBuildingPrefab);
                meleeFriendsBuild.SetJsonData(friendsBuildJson);
                _buildRegistry.Register(meleeFriendsBuild);
                friendsBuildControllers.Add(meleeFriendsBuild);
            }
            return friendsBuildControllers;
        }
        
        public List<FriendsBuildController> CreateDistanceFriendBuilding(IEnumerable<FriendsBuildJson> friendsBuildJsons)
        {
            var friendsBuildControllers = new List<FriendsBuildController>();
            foreach (var friendsBuildJson in friendsBuildJsons)
            {
                var distanceFriendBuild = _resolver.Instantiate(_buildingPrefabConfig.distanceFriendBuildingPrefab);
                distanceFriendBuild.SetJsonData(friendsBuildJson);
                _buildRegistry.Register(distanceFriendBuild);
                friendsBuildControllers.Add(distanceFriendBuild);
            }
            return friendsBuildControllers;
        }

        private MoneyBuildController CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            var moneyBuild = _resolver.Instantiate(_buildingPrefabConfig.moneyBuildingPrefab, position, rotation);
            _buildRegistry.Register(moneyBuild);
            return moneyBuild;
        }
        
        private TowerDefenceController CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            var towerDefence = _resolver.Instantiate(_buildingPrefabConfig.towerBuildingPrefab, position, rotation);
            _buildRegistry.Register(towerDefence);
            return towerDefence;
        }
        
        private FriendsBuildController CreateMeleeFriendBuilding(Vector3 position, Quaternion rotation)
        {
            var meleeFriendsBuild = _resolver.Instantiate(_buildingPrefabConfig.meleeFriendBuildingPrefab, position, rotation);
            _buildRegistry.Register(meleeFriendsBuild);
            return meleeFriendsBuild;
        }
        
        private FriendsBuildController CreateDistanceFriendBuilding(Vector3 position, Quaternion rotation)
        {
            var distanceFriendBuild = _resolver.Instantiate(_buildingPrefabConfig.distanceFriendBuildingPrefab, position, rotation);
            _buildRegistry.Register(distanceFriendBuild);
            return distanceFriendBuild;
        }
    }
}