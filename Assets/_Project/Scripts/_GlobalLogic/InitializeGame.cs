using _Project.Scripts.Factories;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class InitializeGame : IStartable
    {
        [Inject] private LevelController _levelController;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private FriendFactory _friendFactory;
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            _levelController.LoadLevel(0);
        }
    }
}