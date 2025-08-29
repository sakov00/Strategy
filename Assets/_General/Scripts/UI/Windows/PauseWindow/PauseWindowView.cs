using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _General.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private PauseWindowViewModel _viewModel;

        [Header("Buttons")]
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        
        protected override BaseWindowViewModel BaseViewModel => _viewModel;

        private void Start()
        {
            _viewModel.HomeCommand.BindTo(_homeButton).AddTo(this);
            _viewModel.RestartCommand.BindTo(_restartButton).AddTo(this);
            _viewModel.ContinueCommand.BindTo(_continueButton).AddTo(this);
        }
    }
}