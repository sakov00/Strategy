using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class FriendFactory
    {
        private readonly IObjectResolver resolver;
        private readonly UnitModel simpleMeleeFriendModelPrefab;
        private readonly UnitModel simpleDistanceFriendModelPrefab;
        
        public FriendFactory(IObjectResolver resolver, UnitModel simpleMeleeFriendModelPrefab, UnitModel simpleDistanceFriendModelPrefab)
        {
            this.resolver = resolver;
            this.simpleMeleeFriendModelPrefab = simpleMeleeFriendModelPrefab;
            this.simpleDistanceFriendModelPrefab = simpleDistanceFriendModelPrefab;
        }
        
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
            return resolver.Instantiate(simpleMeleeFriendModelPrefab, position, rotation);
        }
        
        private UnitModel CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(simpleDistanceFriendModelPrefab, position, rotation);
        }
    }
}