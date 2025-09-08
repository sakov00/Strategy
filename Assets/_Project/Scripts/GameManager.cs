using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.Pools;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] protected Transform _buildPoolContainer;
        [SerializeField] protected Transform _characterPoolContainer;
        [SerializeField] protected Transform _projectilePoolContainer;
        
        [Inject] protected SaveLoadProgressService SaveLoadProgressService;
        [Inject] protected WindowsManager WindowsManager;
        [Inject] protected ObjectsRegistry ObjectsRegistry;
        [Inject] protected BuildPool BuildPool;
        [Inject] protected CharacterPool CharacterPool;
        [Inject] protected ProjectilePool ProjectilePool;
        
        public virtual void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            WindowsManager.ShowWindow<GameWindowPresenter>();
            BuildPool.SetContainer(_buildPoolContainer);
            CharacterPool.SetContainer(_characterPoolContainer);
            ProjectilePool.SetContainer(_projectilePoolContainer);
            var playerController = CharacterPool.Get<PlayerController>(new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
            //StartLevel(0).Forget();
        }

        public virtual async UniTask StartLevel(int levelIndex)
        {
            await SaveLoadProgressService.LoadLevel(levelIndex);
        }
    }
}