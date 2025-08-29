using System.Collections.Generic;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.SO;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class OthersFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private OthersPrefabConfig _othersPrefabConfig;
        
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        public List<BuildingZoneController> CreateBuildingZones(List<BuildingZoneJson> buildingZoneJsons)
        {
            var buildingZones = new List<BuildingZoneController>();
            foreach (var buildingZoneJson in buildingZoneJsons)
            {
                buildingZones.Add(CreateBuildingZone(buildingZoneJson));
            }
            return buildingZones;
        }
        
        public BuildingZoneController CreateBuildingZone(BuildingZoneJson buildingZoneJson)
        {
            var buildingZoneController = _resolver.Instantiate(_othersPrefabConfig.buildingZonePrefab);
            buildingZoneController.SetJsonData(buildingZoneJson);
            return buildingZoneController;
        }
    }
}