using _Project.Scripts.Factories;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameWindow;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class GameManager : IStartable
    {
        [Inject] private LevelController _levelController;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private FriendFactory _friendFactory;
        [Inject] private EnemyRoadRegistry _enemyRoadRegistry;
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            _friendFactory.CreatePlayer(new Vector3(60, 1, 70));
            //StartLevel(0).Forget();
        }

        public async UniTask StartLevel(int levelIndex)
        {
            await _levelController.LoadLevel(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            _levelController.SaveLevel(levelIndex);
        }
        
        public void NextRound()
        {
            foreach (var spawnPoint in _enemyRoadRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
    }
}