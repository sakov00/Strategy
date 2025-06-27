using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Camera;
using _Project.Scripts.Services;
using _Project.Scripts.Windows;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class InitializeGame : IStartable
    {
        [Inject] private LevelController _levelController;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private FriendFactory _friendFactory;
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            
            var player = _friendFactory.CreatePlayer(new Vector3(50, 10, 50));
            var cameraController = GlobalObjects.CameraController;
            cameraController.cameraFollow.Init(cameraController.transform, player.transform);
            
            _levelController.LoadLevelOnScene(0);
        }
    }
}