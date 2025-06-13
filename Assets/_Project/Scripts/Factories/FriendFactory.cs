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
        
        public UnitController CreateFriendUnit(FriendUnitType unitType, Vector3 position, Quaternion rotation = default)
        {
            UnitController friendContoller;
            switch (unitType)
            {
                case FriendUnitType.SimpleMelee:
                    friendContoller = CreateMeleeFriend(position, rotation);
                    break;
                case FriendUnitType.SimpleDistance:
                    friendContoller = CreateDistanceFriend(position, rotation);
                    break;
                default: return null;
            }
            
            GlobalObjects.GameData.allDamagables.Add(friendContoller.Model);
            return friendContoller;
        }

        private UnitController CreateMeleeFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeFriendPrefab, position, rotation);
        }
        
        private UnitController CreateDistanceFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceFriendPrefab, position, rotation);
        }

        public PlayerController CreatePlayer(Vector3 position, Quaternion rotation = default)
        {
            var playerController = resolver.Instantiate(unitPrefabConfig.playerPrefab, position, rotation);
            GlobalObjects.GameData.allDamagables.Add(playerController.Model);
            return playerController;
        }
    }
}