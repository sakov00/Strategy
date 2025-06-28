using System;
using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
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
        
        [Inject] private ResetService _resetService;
        [Inject] private OthersFactory _othersFactory;
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
                _othersFactory.CreateSpawnPoints(levelJson.spawnDataJsons);
        }
        
        public void SaveLevel()
        {
            var spawnDataJsons = _spawnRegistry.GetAll().Select(x => x.GetJsonData()).ToList();
            var buildingZoneJsons = _buildingZoneRegistry.GetAll().Select(x => x.GetJsonData()).ToList();
            var levelJson = new LevelJson
            {
                spawnDataJsons = spawnDataJsons,
                buildingZoneJsons = buildingZoneJsons,
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