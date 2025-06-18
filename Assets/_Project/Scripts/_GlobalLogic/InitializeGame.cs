using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.Windows;
using _Project.Scripts.Windows.Presenters;
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
            var player = friendFactory.CreatePlayer(new Vector3(50, 10, 50));
            var cameraController = GlobalObjects.CameraController;
            cameraController.cameraFollow.Init(cameraController.transform, player.transform);
        }
    }
}