using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Player;
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
            UnitModel friendModel;
            switch (unitType)
            {
                case FriendUnitType.SimpleMelee:
                    friendModel = CreateMeleeFriend(position, rotation);
                    break;
                case FriendUnitType.SimpleDistance:
                    friendModel = CreateDistanceFriend(position, rotation);
                    break;
                default: return null;
            }
            
            GlobalObjects.GameData.allDamagables.Add(friendModel);
            return friendModel;
        }

        private UnitModel CreateMeleeFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeFriendPrefab, position, rotation);
        }
        
        private UnitModel CreateDistanceFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceFriendPrefab, position, rotation);
        }

        public PlayerModel CreatePlayer(Vector3 position, Quaternion rotation = default)
        {
            var playerModel = resolver.Instantiate(unitPrefabConfig.playerPrefab, position, rotation);
            GlobalObjects.GameData.allDamagables.Add(playerModel);
            return playerModel;
        }
    }
}