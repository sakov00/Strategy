using _Project.Scripts.Factories;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameWindow;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class GameManager : IStartable
    {
        [Inject] private ResetService _resetService;
        [Inject] private LevelController _levelController;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private FriendFactory _friendFactory;
        [Inject] private SpawnRegistry _spawnRegistry;
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            StartLevel(0);
        }

        public void StartLevel(int levelIndex)
        {
            _resetService.ResetLevel();
            _levelController.LoadLevel(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            _levelController.SaveLevel(levelIndex);
            _resetService.ResetLevel();
        }
        
        public void NextRound()
        {
            foreach (var spawnPoint in _spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
    }
}