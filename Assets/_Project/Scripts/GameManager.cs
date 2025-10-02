using System;
using System.Linq;
using System.Threading;
using _General.Scripts;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _General.Scripts.UI.Windows.LoadingWindow;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.Unit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class GameManager : IAsyncStartable, IDisposable
    {
        [Inject] protected AppData AppData;
        [Inject] protected SaveLoadLevelService SaveLoadLevelService;
        [Inject] protected ResetLevelService ResetLevelService;
        [Inject] protected SceneCreator SceneCreator;
        [Inject] protected WindowsManager WindowsManager;
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        [Inject] protected ApplicationEventsHandler ApplicationEventsHandler;
        
        public virtual async UniTask StartAsync(CancellationToken cancellation = default)
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            WindowsManager.ShowFastWindow<LoadingWindowPresenter>();
            WindowsManager.ShowFastWindow<GameWindowPresenter>();
            await StartLevel(AppData.User.CurrentLevel);
        }

        public virtual async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.GetWindow<GameWindowPresenter>().Dispose();
            await WindowsManager.ShowWindow<LoadingWindowPresenter>();
            AppData.LevelEvents.Dispose();
            AppData.LevelEvents.Initialize();
            await LoadLevel(levelIndex);
            ApplicationEventsHandler.OnApplicationQuited += OnApplicationQuit;
            ApplicationEventsHandler.OnApplicationPaused += OnApplicationPause;
            var playerController = ObjectsRegistry.GetTypedList<UnitController>().First(x => x.UnitType == UnitType.Player);
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
            WindowsManager.ShowFastWindow<GameWindowPresenter>();
            await WindowsManager.HideWindow<LoadingWindowPresenter>();
        }
        
        public virtual async UniTask LoadLevel(int levelIndex)
        {
            ResetLevelService.ResetLevel();
            var levelModel = await SaveLoadLevelService.LoadLevel(levelIndex);
            await SceneCreator.InstantiateLoadedObjects(levelModel);
            AppData.User.CurrentLevel = levelIndex;
            AppData.User.CurrentRound = levelModel.CurrentRound;
            AppData.LevelData.LevelMoney = levelModel.LevelMoney;
            AppData.LevelData.IsFighting = levelModel.IsFighting;
        }
        
        private void OnApplicationQuit()
        {
            SaveLoadLevelService?.SaveLevelProgress(AppData.User.CurrentLevel).Forget();
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (pause)
                SaveLoadLevelService?.SaveLevelProgress(AppData.User.CurrentLevel).Forget();
        }
        
        public void Dispose()
        {
            ApplicationEventsHandler.OnApplicationQuited -= OnApplicationQuit;
            ApplicationEventsHandler.OnApplicationPaused -= OnApplicationPause;
        }
    }
}