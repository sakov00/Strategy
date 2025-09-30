using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.DTO;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Enums;
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
        [Inject] protected ResetLevelService ResetLevelService;
        [Inject] protected SceneCreator SceneCreator;
        [Inject] protected WindowsManager WindowsManager;
        [Inject] protected UnitPool UnitPool;
        
        public virtual void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            StartLevel(AppData.User.CurrentLevel).Forget();
        }

        public virtual async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.GetWindow<GameWindowPresenter>().Dispose();
            WindowsManager.ShowWindow<GameWindowPresenter>();
            ResetLevelService.ResetLevel();
            AppData.LevelEvents.Dispose();
            AppData.LevelEvents.Initialize();
            AppData.User.CurrentLevel = levelIndex;
            AppData.User.CurrentRound = 0;
            var levelModel = await LoadLevel(levelIndex);
            await SceneCreator.InstantiateLoadedObjects(levelModel);
            WindowsManager.GetWindow<GameWindowPresenter>().Initialize();
            var playerController = UnitPool.Get(UnitType.Player, new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
        }
        
        public virtual async UniTask<LevelModel> LoadLevel(int levelIndex)
        {
            return await SaveLoadLevelService.LoadLevelDefault(levelIndex);
        }
    }
}