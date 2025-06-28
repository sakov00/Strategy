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
using _Project.Scripts.SpawnPoints;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class OthersFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private OthersPrefabConfig _othersPrefabConfig;
        
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private BuildingZoneRegistry _buildingZoneRegistry;
        
        
        public List<SpawnPoint> CreateSpawnPoints(List<SpawnDataJson> spawnDataJsons)
        {
            var spawnPoints = new List<SpawnPoint>();
            foreach (var spawnDataJson in spawnDataJsons)
            {
                spawnPoints.Add(CreateSpawnPoint(spawnDataJson));
            }
            return spawnPoints;
        }
        
        public SpawnPoint CreateSpawnPoint(SpawnDataJson spawnDataJson)
        {
            var spawnPoint = _resolver.Instantiate(_othersPrefabConfig.spawnPointPrefab);
            spawnPoint.SetJsonData(spawnDataJson);
            _spawnRegistry.Register(spawnPoint);
            return spawnPoint;
        }
        
        public List<BuildingZoneController> CreateBuildingZones(List<BuildingZoneJson> buildingZoneJsons)
        {
            var buildingZones = new List<BuildingZoneController>();
            foreach (var buildingZoneJson in buildingZoneJsons)
            {
                buildingZones.Add(CreateBuildingZone(buildingZoneJson));
            }
            return buildingZones;
        }
        
        public BuildingZoneController CreateBuildingZone(BuildingZoneJson buildingZoneJson)
        {
            var buildingZoneController = _resolver.Instantiate(_othersPrefabConfig.buildingZonePrefab);
            buildingZoneController.SetJsonData(buildingZoneJson);
            _buildingZoneRegistry.Register(buildingZoneController);
            return buildingZoneController;
        }
    }
}