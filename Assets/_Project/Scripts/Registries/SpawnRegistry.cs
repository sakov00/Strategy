using System.Collections.Generic;
using _Project.Scripts.SpawnPoints;

namespace _Project.Scripts.Registries
{
    public class SpawnRegistry
    {
        private readonly HashSet<SpawnPoint> spawnPoints = new();

        public void Register(SpawnPoint spawnPoint)
        {
            spawnPoints.Add(spawnPoint);
        }

        public void Unregister(SpawnPoint spawnPoint)
        {
            spawnPoints.Remove(spawnPoint);
        }

        public HashSet<SpawnPoint> GetAll()
        {
            return spawnPoints;
        }

        public void Clear()
        {
            spawnPoints.Clear();
        }
    }
}