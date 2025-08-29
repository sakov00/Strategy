using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.Units.Friends.Player;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class GameManager : IStartable
    {
        [Inject] private LevelSaveLoadService _levelSaveLoadService;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            _buildPool.SetContainer(_objectPoolContainer);
            _buildPool.Get<PlayerController>(new Vector3(60, 1, 70));
            //StartLevel(0).Forget();
        }

        public async UniTask StartLevel(int levelIndex)
        {
            await _levelSaveLoadService.LoadLevel(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            _levelSaveLoadService.SaveLevel(levelIndex);
        }
        
        public void NextRound()
        {
            foreach (var spawnPoint in _objectsRegistry.GetTypedList<EnemyRoad>())
            {
                spawnPoint.StartSpawn();
            }
        }
    }
}