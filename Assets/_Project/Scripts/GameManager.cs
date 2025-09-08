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
        [SerializeField] private Transform _buildPoolContainer;
        [SerializeField] private Transform _characterPoolContainer;
        [SerializeField] private Transform _projectilePoolContainer;
        
        [Inject] private SaveLoadProgressService _saveLoadProgressService;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private ProjectilePool _projectilePool;
        
        public void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            _buildPool.SetContainer(_buildPoolContainer);
            _characterPool.SetContainer(_characterPoolContainer);
            _projectilePool.SetContainer(_projectilePoolContainer);
            var playerController = _characterPool.Get<PlayerController>(new Vector3(60, 1, 70));
            GlobalObjects.CameraController.CameraFollow.Init(GlobalObjects.CameraController.transform, playerController.transform);
            //StartLevel(0).Forget();
        }

        public async UniTask StartLevel(int levelIndex)
        {
            await _saveLoadProgressService.LoadLevel(levelIndex);
        }
    }
}