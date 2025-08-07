using System.Collections.Generic;
using _Project.Scripts.GameObjects.SpawnPoints;

namespace _Project.Scripts.Registries
{
    public class SpawnRegistry
    {
        private readonly HashSet<SpawnPoint> _spawnPoints = new();

        public void Register(SpawnPoint spawnPoint)
        {
            if (!_spawnPoints.Contains(spawnPoint))
                _spawnPoints.Add(spawnPoint);
        }

        public void Unregister(SpawnPoint spawnPoint)
        {
            if (_spawnPoints.Contains(spawnPoint))
                _spawnPoints.Remove(spawnPoint);
        }

        public HashSet<SpawnPoint> GetAll()
        {
            return _spawnPoints;
        }

        public void Clear()
        {
            _spawnPoints.Clear();
        }
    }
}