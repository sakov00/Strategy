using System.Collections.Generic;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.SpawnPoints;

namespace _Project.Scripts.Json
{
    public class LevelJson
    {
        public List<SpawnPoint> spawnPoints;
        public List<BuildingZoneModel> buildingZoneModels;
    }
}