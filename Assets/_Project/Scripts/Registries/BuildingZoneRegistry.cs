using _Project.Scripts.GameObjects.BuildingZone;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Registries
{
    public class BuildingZoneRegistry
    {
        private readonly ReactiveCollection<BuildingZoneModel> _buildingZoneModels = new();

        public void Register(BuildingZoneModel damageable)
        {
            _buildingZoneModels.Add(damageable);
        }

        public void Unregister(BuildingZoneModel damageable)
        {
            _buildingZoneModels.Remove(damageable);
        }

        public IReactiveCollection<BuildingZoneModel> GetAll()
        {
            return _buildingZoneModels;
        }

        public void Clear()
        {
            _buildingZoneModels.Clear();
        }
    }
}