using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.SpawnPoints;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Json
{
    [Serializable]
    public class LevelJson
    {
        public List<SpawnDataJson> spawnDataJsons;
        public List<BuildingZoneJson> buildingZoneJsons;
        public List<MoneyBuildJson> moneyBuildJsons;
        public List<FriendsBuildJson> friendsBuildJsons;
        public List<TowerDefenceBuildJson> towerDefenceBuildJsons;
    }
    
    [Serializable]
    public class SpawnDataJson
    {
        public Vector3 position;
        public float spawnRadius;
        public List<EnemyGroup> roundEnemyList = new();
    }
    
    [Serializable]
    public class BuildingZoneJson
    {
        public Vector3 position;
        public TypeBuilding typeBuilding;
    }
    
    [Serializable]
    public class MoneyBuildJson
    {
        public Vector3 position;
        public Quaternion rotation;
        public MoneyBuildModel moneyBuildModel = new();
    }
        
    [Serializable]
    public class FriendsBuildJson
    {
        public Vector3 position;
        public Quaternion rotation;
        public FriendsBuildModel friendsBuildModel = new();
    }
        
    [Serializable]
    public class TowerDefenceBuildJson
    {
        public Vector3 position;
        public Quaternion rotation;
        public TowerDefenceModel towerDefenceModel = new();
    }
}