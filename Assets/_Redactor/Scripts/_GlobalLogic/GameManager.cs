using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Redactor.Scripts._GlobalLogic
{
    public class RedactorManager : IStartable
    {
        [Inject] private LevelSaveLoadService _levelSaveLoadService;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private FriendFactory _friendFactory;
        
        public void Start()
        {
            Application.targetFrameRate = 120;
            _windowsManager.ShowWindow<GameWindowView>();
            _friendFactory.CreatePlayer(new Vector3(60, 1, 70));
            //StartLevel(0).Forget();
        }
    }
}