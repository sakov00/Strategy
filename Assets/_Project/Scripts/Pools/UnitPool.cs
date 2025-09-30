using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Abstract.Unit;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Pools
{
    public class UnitPool
    {
        [Inject] private UnitFactory _unitFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;

        private Transform _containerTransform;
        private readonly List<UnitController> _availableUnits = new();

        public void SetContainer(Transform transform)
        {
            _containerTransform = transform;
        }
        
        public List<UnitController> GetAvailableUnits() => _availableUnits;
        
        public UnitController Get(UnitType unitType, Vector3 position = default, Quaternion rotation = default) 
        {
            var unit = _availableUnits.FirstOrDefault(c => c.UnitType == unitType);
            if (unit != null)
            {
                _availableUnits.Remove(unit);
                unit.transform.position = position;
                unit.transform.rotation = rotation;
                unit.gameObject.SetActive(true);
            }
            else
            {
                unit = _unitFactory.CreateUnit(unitType, position, rotation);
                
            }
            unit.Initialize();
            return unit;
        }

        public void Return<T>(T character) where T : UnitController
        {
            if (!_availableUnits.Contains(character))
            {
                _availableUnits.Add(character);
            }

            character.gameObject.SetActive(false);
            character.transform.SetParent(_containerTransform, false);
            _objectsRegistry.Unregister<UnitController>(character);
        }
        
        public void Remove<T>(T build) where T : UnitController
        {
            if (!_availableUnits.Contains(build))
            {
                _availableUnits.Remove(build);
            }
        }
    }
}
