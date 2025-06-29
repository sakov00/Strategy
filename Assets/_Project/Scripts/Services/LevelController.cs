using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.SpawnPoints;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Services
{
    public class LevelController : IInitializable
    {
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private SaveRegistry _saveRegistry;
        
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
            // var levelJson = _jsonLoader.Load<LevelJson>(index);
            // if (levelJson != null)
            // {
            //     //TODO add player and unit
            //     _othersFactory.CreateSpawnPoints(levelJson.spawnDataJsons);
            //     _othersFactory.CreateBuildingZones(levelJson.buildingZoneJsons);
            //     _buildFactory.CreateMoneyBuilding(levelJson.moneyBuildJsons);
            //     _buildFactory.CreateMeleeFriendBuilding(levelJson.friendsBuildJsons
            //         .Where(x => x.friendsBuildModel.unitType == FriendUnitType.SimpleMelee));
            //     _buildFactory.CreateDistanceFriendBuilding(levelJson.friendsBuildJsons
            //         .Where(x => x.friendsBuildModel.unitType == FriendUnitType.SimpleDistance));
            //     _buildFactory.CreateTowerDefenseBuilding(levelJson.towerDefenceBuildJsons);
            // }
        }
        
        public void SaveLevel()
        {
            var spawnDataJsons = _saveRegistry.GetAll<SpawnPoint>().Select(x => x.GetJsonData()).ToList();
            var buildingZoneJsons = _saveRegistry.GetAll<BuildingZoneController>().Select(x => x.GetJsonData()).ToList();
            var moneyBuildJsons = _saveRegistry.GetAll<MoneyBuildController>().Select(x => x.GetJsonData()).ToList();
            var friendsBuildJsons = _saveRegistry.GetAll<FriendsBuildController>().Select(x => x.GetJsonData()).ToList();
            var towerDefenceBuildJsons = _saveRegistry.GetAll<TowerDefenceController>().Select(x => x.GetJsonData()).ToList();
            var playerJsons = _saveRegistry.GetAll<PlayerController>().Select(x => x.GetJsonData()).ToList();
            var unitJsons = _saveRegistry.GetAll<UnitController>().Select(x => x.GetJsonData()).ToList();
            var levelJson = new LevelJson
            {
                spawnDataJsons = spawnDataJsons,
                buildingZoneJsons = buildingZoneJsons,
                moneyBuildJsons = moneyBuildJsons,
                friendsBuildJsons = friendsBuildJsons,
                towerDefenceBuildJsons = towerDefenceBuildJsons,
                playerJsons = playerJsons,
                unitJsons = unitJsons,
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