using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.SpawnPoints;
using _Project.Scripts.GameObjects.TowerDefence;
using UnityEngine;

namespace _Project.Scripts.Json
{
    [Serializable]
    public class LevelJson
    {
        public List<EnvironmentJson> environments;
        public List<BuildingZoneJson> buildingZoneJsons;
        public List<MoneyBuildJson> moneyBuildJsons;
        public List<FriendsBuildJson> friendsBuildJsons;
        public List<TowerDefenceBuildJson> towerDefenceBuildJsons;
        public List<PlayerJson> playerJsons;
    }
    
    [Serializable]
    public abstract class ConcreteObjectJson
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    [Serializable]
    public class BuildingZoneJson : ConcreteObjectJson
    {
        public TypeBuilding typeBuilding;
    }
    
    [Serializable]
    public class MoneyBuildJson : ConcreteObjectJson
    {
        public MoneyBuildModel moneyBuildModel;
    }
        
    [Serializable]
    public class FriendsBuildJson : ConcreteObjectJson
    {
        public FriendsBuildModel friendsBuildModel;
        public List<UnitJson> unitJsons;
    }
        
    [Serializable]
    public class TowerDefenceBuildJson : ConcreteObjectJson
    {
        public TowerDefenceModel towerDefenceModel;
    }
    
    [Serializable]
    public class PlayerJson : ConcreteObjectJson
    {
        public PlayerModel playerModel;
    }
    
    [Serializable]
    public class UnitJson : ConcreteObjectJson
    {
        public UnitModel unitModel;
    }
    
    [Serializable]
    public class EnvironmentJson : ConcreteObjectJson
    {
        public EnvironmentType environmentType;
    }
}