using System.Threading;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts;
using _Redactor.Scripts.LevelRedactorWindow;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Redactor.Scripts
{
    public class RedactorManager : GameManager
    {
        public override async UniTask StartAsync(CancellationToken cancellation = default)
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            WindowsManager.ShowFastWindow<LevelRedactorWindowPresenter>();
        }
        
        public override async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.HideFastWindow<LevelRedactorWindowPresenter>();
            await base.StartLevel(levelIndex);
        }

        public override async UniTask LoadLevel(int levelIndex)
        {
            ResetService.ResetLevel();
            await SaveLoadLevelService.LoadLevelDefault(levelIndex);
            await SceneCreator.InstantiateObjects(AppData.LevelData.SavableModels);
        }
        
        public void SaveLevel(int levelIndex)
        {
            SaveLoadLevelService.SaveLevelDefault(levelIndex).Forget();
        }
    }
}