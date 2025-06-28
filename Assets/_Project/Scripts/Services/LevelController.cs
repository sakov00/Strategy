using System;
using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Windows.Presenters;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Services
{
    public class LevelController : IInitializable
    {
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private BuildingZoneRegistry _buildingZoneRegistry;
        [Inject] private BuildRegistry _buildRegistry;
        
        [Inject] private ResetService _resetService;
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private JsonLoader _jsonLoader;
        
        public readonly ReactiveCommand RoundUpdated = new();

        public void Initialize()
        {
            _healthRegistry
                .GetAll()
                .ObserveRemove()
                .Subscribe(_ => TryStartNewRound());
        }

        public void LoadLevelOnScene(int index)
        {
            var levelJson = _jsonLoader.Load<LevelJson>(index);
            if (levelJson != null)
            {
                _othersFactory.CreateSpawnPoints(levelJson.spawnDataJsons);
                _othersFactory.CreateBuildingZones(levelJson.buildingZoneJsons);
                _buildFactory.CreateMoneyBuilding(levelJson.moneyBuildJsons);
                _buildFactory.CreateMeleeFriendBuilding(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.unitType == FriendUnitType.SimpleMelee));
                _buildFactory.CreateDistanceFriendBuilding(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.unitType == FriendUnitType.SimpleDistance));
                _buildFactory.CreateTowerDefenseBuilding(levelJson.towerDefenceBuildJsons);
            }
        }
        
        public void SaveLevel()
        {
            var spawnDataJsons = _spawnRegistry.GetAll().Select(x => x.GetJsonData()).ToList();
            var buildingZoneJsons = _buildingZoneRegistry.GetAll().Select(x => x.GetJsonData()).ToList();
            var moneyBuildJsons = _buildRegistry.GetAll<MoneyBuildController>().Select(x => x.GetJsonData()).ToList();
            var friendsBuildJsons = _buildRegistry.GetAll<FriendsBuildController>().Select(x => x.GetJsonData()).ToList();
            var towerDefenceBuildJsons = _buildRegistry.GetAll<TowerDefenceController>().Select(x => x.GetJsonData()).ToList();
            var levelJson = new LevelJson
            {
                spawnDataJsons = spawnDataJsons,
                buildingZoneJsons = buildingZoneJsons,
                moneyBuildJsons = moneyBuildJsons,
                friendsBuildJsons = friendsBuildJsons,
                towerDefenceBuildJsons = towerDefenceBuildJsons
            };
            _jsonLoader.Save(levelJson, 0);
        }

        public void NextRound()
        {
            foreach (var spawnPoint in _spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
        
        private void TryStartNewRound()
        {
            var enemiesRemain = _healthRegistry.GetAll().Any(unit => unit.WarSide == WarSide.Enemy);
            if (enemiesRemain) return;

            _resetService.NewRound();
            RoundUpdated.Execute();
        }
    }
}