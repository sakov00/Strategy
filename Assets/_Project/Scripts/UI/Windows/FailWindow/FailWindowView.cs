using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows.FailWindow
{
    public class FailWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private FailWindowViewModel _viewModel;

        [Header("Buttons")]
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;

        protected override BaseWindowViewModel BaseViewModel => _viewModel;
        
        private void Start()
        {
            _viewModel.HomeCommand.BindTo(_homeButton).AddTo(this);
            _viewModel.RestartCommand.BindTo(_restartButton).AddTo(this);
        }
    }
}