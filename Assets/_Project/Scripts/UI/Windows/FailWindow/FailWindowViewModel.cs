using _Project.Scripts._GlobalLogic;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.UI.Windows.FailWindow
{
    public class FailWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private FailWindowModel _model;
        
        [Inject] private GameManager _gameManager;
        
        protected override BaseWindowModel BaseModel => _model;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        
        

        private void Awake()
        {
            IsPaused.Subscribe(PauseGame).AddTo(this);
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(this);
            RestartCommand.Subscribe(_ => RestartOnClick()).AddTo(this);
        }
        
        private void HomeOnClick()
        {
        }
        
        private void RestartOnClick()
        {
            _gameManager.StartLevel(0);
            WindowsManager.HideWindow<FailWindowView>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}