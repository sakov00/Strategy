using _Project.Scripts.GameObjects.BuildingZone;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Registries
{
    public class BuildingZoneRegistry
    {
        private readonly ReactiveCollection<BuildingZoneController> _buildingZoneModels = new();

        public void Register(BuildingZoneController damageable)
        {
            _buildingZoneModels.Add(damageable);
        }

        public void Unregister(BuildingZoneController damageable)
        {
            _buildingZoneModels.Remove(damageable);
        }

        public IReactiveCollection<BuildingZoneController> GetAll()
        {
            return _buildingZoneModels;
        }

        public void Clear()
        {
            _buildingZoneModels.Clear();
        }
    }
}