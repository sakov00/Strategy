using System.Linq;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using VContainer;

namespace _Project.Scripts.Services
{
    public class LevelController
    {
        [Inject] private JsonLoader<LevelJson> _jsonLoader;
        [Inject] private BuildingZoneRegistry _buildingZoneRegistry;
        [Inject] private SpawnRegistry _spawnRegistry;

        public void LoadLevelOnScene(int index)
        {
            _jsonLoader.Load();
        }
    }
}