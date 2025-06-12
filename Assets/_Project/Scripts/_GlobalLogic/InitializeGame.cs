using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class InitializeGame : IStartable
    {
        [Inject] private WindowFactory windowFactory;
        [Inject] private FriendFactory friendFactory;
        
        public void Start()
        {
            var gameWindow = windowFactory.CreateWindow<GameWindow>();
            GlobalObjects.GameData.gameWindow = gameWindow;
            
            var player = friendFactory.CreatePlayer(new Vector3(50, 10, 50));
            GlobalObjects.CameraFollow.target = player.transform;
        }
    }
}