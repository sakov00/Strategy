using _General.Scripts.Registries;
using _Project.Scripts.GameObjects._Object.BuildingZone;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class OthersFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private OthersPrefabConfig _othersPrefabConfig;
        
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        public BuildingZone CreateBuildingZone(Vector3 position = default, Quaternion rotation = default)
        {
            var buildingZoneController = _resolver.Instantiate(_othersPrefabConfig.buildingZonePrefab, position, rotation);
            return buildingZoneController;
        }
    }
}