using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class FriendFactory
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private UnitPrefabConfig unitPrefabConfig;
        
        public UnitModel CreateFriendUnit(FriendUnitType unitType, Vector3 position, Quaternion rotation = default)
        {
            switch (unitType)
            {
                case FriendUnitType.SimpleMelee:
                    return CreateMoneyBuilding(position, rotation);
                case FriendUnitType.SimpleDistance:
                    return CreateTowerDefenseBuilding(position, rotation);
                default: return null;
            }
        }

        private UnitModel CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeFriendPrefab, position, rotation);
        }
        
        private UnitModel CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceFriendPrefab, position, rotation);
        }
    }
}