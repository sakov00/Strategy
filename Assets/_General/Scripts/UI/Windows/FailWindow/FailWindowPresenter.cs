using _Project.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.FailWindow
{
    public class FailWindowPresenter : BaseWindowPresenter
    {
        [SerializeField] private FailWindowModel _model;
        [SerializeField] private FailWindowView _view;
        
        [Inject] private GameManager _gameManager;
        
        protected override BaseWindowModel BaseModel => _model;
        protected override BaseWindowView BaseView => _view;

        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        
        

        protected override void Awake()
        {
            base.Awake();
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
            WindowsManager.HideWindow<FailWindowPresenter>();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}