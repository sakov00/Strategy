using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
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
        [Inject] private HealthRegistry healthRegistry;
        
        public UnitController CreateFriendUnit(UnitType unitType, Vector3 position, Quaternion rotation = default)
        {
            UnitController friendContoller;
            switch (unitType)
            {
                case UnitType.SimpleMelee:
                    friendContoller = CreateMeleeFriend(position, rotation);
                    break;
                case UnitType.SimpleDistance:
                    friendContoller = CreateDistanceFriend(position, rotation);
                    break;
                default: return null;
            }
            friendContoller.SetNoAimPosition(new List<Vector3>{position});
            healthRegistry.Register(friendContoller.ObjectModel);
            return friendContoller;
        }
        
        public List<UnitController> CreateFriendUnits(List<UnitJson> unitJsons)
        {
            var unitControllers = new List<UnitController>();
            foreach (var unitJson in unitJsons)
            {
                var unit = CreateFriendUnit(unitJson.unitModel.UnitType, unitJson.unitModel.WayToAim.First());
                unit.SetJsonData(unitJson);
                unitControllers.Add(unit);
            }
            return unitControllers;
        }

        private UnitController CreateMeleeFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeFriendPrefab, position, rotation);
        }
        
        private UnitController CreateDistanceFriend(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceFriendPrefab, position, rotation);
        }
        
        public List<PlayerController> CreatePlayers(List<PlayerJson> playerJsons)
        {
            var players = new List<PlayerController>();
            foreach (var playerJson in playerJsons)
            {
                var player = CreatePlayer();
                player.SetJsonData(playerJson);
                players.Add(player);
            }
            return players;
        }

        public PlayerController CreatePlayer(Vector3 position = default, Quaternion rotation = default)
        {
            var playerController = resolver.Instantiate(unitPrefabConfig.playerPrefab, position, rotation);
            healthRegistry.Register(playerController.ObjectModel);
            var cameraController = GlobalObjects.CameraController;
            cameraController._cameraFollow.Init(cameraController.transform, playerController.transform);
            return playerController;
        }
    }
}