using System.Collections.Generic;
using _Project.Scripts.GameObjects.EnemyRoads;

namespace _Project.Scripts.Registries
{
    public class EnemyRoadRegistry
    {
        private readonly HashSet<EnemyRoad> _spawnPoints = new();

        public void Register(EnemyRoad enemyRoad)
        {
            if (!_spawnPoints.Contains(enemyRoad))
                _spawnPoints.Add(enemyRoad);
        }

        public void Unregister(EnemyRoad enemyRoad)
        {
            if (_spawnPoints.Contains(enemyRoad))
                _spawnPoints.Remove(enemyRoad);
        }

        public HashSet<EnemyRoad> GetAll()
        {
            return _spawnPoints;
        }

        public void Clear()
        {
            _spawnPoints.Clear();
        }
    }
}