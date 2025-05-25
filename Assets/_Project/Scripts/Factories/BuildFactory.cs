using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class BuildFactory
    {
        private readonly IObjectResolver resolver;
        private readonly MoneyBuildModel moneyBuildModelPrefab;
        private readonly TowerDefenceModel towerDefenceModelPrefab;
        private readonly FriendsBuildModel meleeFriendsBuildModelPrefab;
        private readonly FriendsBuildModel distanceFriendsBuildModelPrefab;
        
        public BuildFactory(IObjectResolver resolver, MoneyBuildModel moneyBuildModelPrefab, TowerDefenceModel towerDefenceModelPrefab,
            FriendsBuildModel meleeFriendsBuildModelPrefab, FriendsBuildModel distanceFriendsBuildModelPrefab)
        {
            this.resolver = resolver;
            this.moneyBuildModelPrefab = moneyBuildModelPrefab;
            this.towerDefenceModelPrefab = towerDefenceModelPrefab;
            this.meleeFriendsBuildModelPrefab = meleeFriendsBuildModelPrefab;
            this.distanceFriendsBuildModelPrefab = distanceFriendsBuildModelPrefab;
        }
        
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
            return resolver.Instantiate(moneyBuildModelPrefab, position, rotation);
        }
        
        private BuildModel CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(towerDefenceModelPrefab, position, rotation);
        }
        
        private BuildModel CreateMeleeFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(meleeFriendsBuildModelPrefab, position, rotation);;
        }
        
        private BuildModel CreateDistanceFriendBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(distanceFriendsBuildModelPrefab, position, rotation);
        }
    }
}