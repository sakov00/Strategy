using _Project.Scripts;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowViewModel : BaseWindowViewModel
    {
        [SerializeField] private PauseWindowModel _model;
        [Inject] private GameManager _gameManager;
        
        protected override BaseWindowModel BaseModel => _model;
        
        public ReactiveCommand HomeCommand { get; } = new();
        public ReactiveCommand RestartCommand { get; } = new();
        public ReactiveCommand ContinueCommand { get; } = new();

        protected override void Awake()
        {
            base.Awake();
            HomeCommand.Subscribe(_ => HomeOnClick()).AddTo(this);
            RestartCommand.Subscribe(_ => RestartOnClick()).AddTo(this);
            ContinueCommand.Subscribe(_ => ContinueOnClick()).AddTo(this);
        }
        
        private void HomeOnClick()
        {
        }
        
        private void RestartOnClick()
        {
            _gameManager.StartLevel(0);
            WindowsManager.HideWindow<PauseWindowView>();
        }
        
        private void ContinueOnClick()
        {
            WindowsManager.HideWindow<PauseWindowView>();
        }
    }
}