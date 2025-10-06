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
        [Inject] protected ResetLevelService ResetService;
        [Inject] protected SceneCreator SceneCreator;
        [Inject] protected WindowsManager WindowsManager;
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        [Inject] protected ApplicationEventsHandler ApplicationEventsHandler;
        
        public virtual async UniTask StartAsync(CancellationToken cancellation = default)
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            WindowsManager.ShowFastWindow<LoadingWindowPresenter>();
            await StartLevel(AppData.User.CurrentLevel);
        }

        public virtual async UniTask ResetRound()
        {
            ResetService.ResetRound();
            await SceneCreator.InstantiateObjects(AppData.LevelData.ObjectsForRestoring);
            WindowsManager.ShowFastWindow<GameWindowPresenter>();
        }

        public virtual async UniTask RestartLevel()
        {
            SaveLoadLevelService.RemoveProgress(AppData.User.CurrentLevel);
            await StartLevel(AppData.User.CurrentLevel);
        }

        public virtual async UniTask StartLevel(int levelIndex)
        {
            Dispose();
            AppData.User.CurrentLevel = levelIndex;
            
            Time.timeScale = 0;
            await WindowsManager.ShowWindow<LoadingWindowPresenter>();
            WindowsManager.HideFastWindow<GameWindowPresenter>();
            
            await LoadLevel(levelIndex);

            var playerController = ObjectsRegistry.GetTypedList<UnitController>().First(x => x.UnitType == UnitType.Player);
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
            
            if(AppData.LevelData.IsFighting) AppData.LevelEvents.Initialize();
            ApplicationEventsHandler.OnApplicationQuited += OnApplicationQuit;
            ApplicationEventsHandler.OnApplicationPaused += OnApplicationPause;
            
            WindowsManager.ShowFastWindow<GameWindowPresenter>();
            await WindowsManager.HideWindow<LoadingWindowPresenter>();
            Time.timeScale = 1;
        }
        
        public virtual async UniTask LoadLevel(int levelIndex)
        {
            // need return bool and handle(exists file or not)
            ResetService.ResetLevel();
            await SaveLoadLevelService.LoadLevel(levelIndex);
            await SceneCreator.InstantiateObjects(AppData.LevelData.SavableModels);
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