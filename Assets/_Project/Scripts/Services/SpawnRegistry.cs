using System.Collections.Generic;
using _Project.Scripts.Interfaces;
using _Project.Scripts.SpawnPoints;
using UniRx;

namespace _Project.Scripts.Services
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