using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
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
            BuildPool.SetContainer(_buildPoolContainer);
            CharacterPool.SetContainer(_characterPoolContainer);
            ProjectilePool.SetContainer(_projectilePoolContainer);
        }
        
        public override async UniTask StartLevel(int levelIndex)
        {
            WindowsManager.HideWindow<LevelRedactorWindowPresenter>();
            await base.StartLevel(levelIndex);
            var playerController = CharacterPool.Get<PlayerController>(new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
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