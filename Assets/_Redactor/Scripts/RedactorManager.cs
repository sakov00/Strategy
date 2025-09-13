using _General.Scripts._VContainer;
using _Project.Scripts;
using _Redactor.Scripts.LevelRedactorWindow;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Redactor.Scripts
{
    public class RedactorManager : GameManager
    {
        public override void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            WindowsManager.ShowWindow<LevelRedactorWindowPresenter>();
        }
        
        public override async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.HideWindow<LevelRedactorWindowPresenter>();
            await base.StartLevel(levelIndex);
        }

        public override async UniTask LoadLevel(int levelIndex)
        {
            await SaveLoadLevelService.LoadLevelDefault(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            SaveLoadLevelService.SaveLevelDefault(levelIndex).Forget();
        }
    }
}