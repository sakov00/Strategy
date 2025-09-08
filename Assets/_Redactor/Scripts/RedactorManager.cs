using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.Pools;
using _Project.Scripts.Services;
using _Redactor.Scripts.LevelRedactorWindow;
using _Redactor.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts
{
    public class RedactorManager : GameManager
    {
        [Inject] protected SaveLoadLevelService SaveLoadLevelService;
        
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
            await SaveLoadLevelService.LoadLevel(levelIndex);
            WindowsManager.HideWindow<LevelRedactorWindowPresenter>();
            WindowsManager.ShowWindow<GameWindowPresenter>();
            var playerController = CharacterPool.Get<PlayerController>(new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
        }

        public async UniTask LoadLevel(int levelIndex)
        {
            await SaveLoadLevelService.LoadLevel(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            SaveLoadLevelService.SaveLevel(levelIndex).Forget();
        }
    }
}