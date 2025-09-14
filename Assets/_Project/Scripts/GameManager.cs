using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class GameManager : IStartable
    {
        [Inject] protected AppData AppData;
        [Inject] protected SaveLoadLevelService SaveLoadLevelService;
        [Inject] protected WindowsManager WindowsManager;
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        [Inject] protected CharacterPool CharacterPool;
        
        public virtual void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            StartLevel(0).Forget();
        }

        public virtual async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.GetWindowOrInstantiate<GameWindowPresenter>().Dispose();
            WindowsManager.ShowWindow<GameWindowPresenter>();
            AppData.LevelEvents.Dispose();
            AppData.LevelEvents.Initialize();
            AppData.LevelData.CurrentLevel = levelIndex;
            AppData.LevelData.CurrentRound = 0;
            await LoadLevel(levelIndex);
            WindowsManager.GetWindow<GameWindowPresenter>().Initialize();
            var playerController = CharacterPool.Get<PlayerController>(new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
        }
        
        public virtual async UniTask LoadLevel(int levelIndex)
        {
            await SaveLoadLevelService.LoadLevelDefault(levelIndex);
        }
    }
}