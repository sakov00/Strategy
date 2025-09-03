using _General.Scripts._VContainer;
using _General.Scripts.UI.Windows;
using _Project.Scripts.Pools;
using _Project.Scripts.Services;
using _Redactor.Scripts.LevelRedactorWindow;
using _Redactor.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts
{
    public class RedactorManager : MonoBehaviour
    {
        [SerializeField] private Transform _buildPoolContainer;
        [SerializeField] private Transform _characterPoolContainer;
        [SerializeField] private Transform _projectilePoolContainer;
        
        [Inject] private SaveLoadLevelService _saveLoadProgressService;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private ProjectilePool _projectilePool;
        
        public void Start()
        {
            InjectManager.Inject(this);
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<LevelRedactorWindowView>();
            _buildPool.SetContainer(_buildPoolContainer);
            _characterPool.SetContainer(_characterPoolContainer);
            _projectilePool.SetContainer(_projectilePoolContainer);
        }

        public async UniTask StartLevel(int levelIndex)
        {
            await _saveLoadProgressService.LoadLevel(levelIndex);
        }
        
        public void SaveLevel(int levelIndex)
        {
            _saveLoadProgressService.SaveLevel(levelIndex).Forget();
        }
    }
}