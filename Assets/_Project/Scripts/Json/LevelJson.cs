using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
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
        public List<BuildJson> buildJsons;
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
    public class BuildJson
    {
        public Vector3 position;
        public List<BuildModel> BuildModels = new();
    }
}