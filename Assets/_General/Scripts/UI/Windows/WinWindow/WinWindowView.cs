using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _General.Scripts.UI.Windows.WinWindow
{
    public class WinWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private WinWindowPresenter _presenter;

        [Header("Buttons")]
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        
        protected override BaseWindowPresenter BasePresenter => _presenter;

        private void Start()
        {
            _presenter.HomeCommand.BindTo(_homeButton).AddTo(this);
            _presenter.RestartCommand.BindTo(_restartButton).AddTo(this);
            _presenter.ContinueCommand.BindTo(_continueButton).AddTo(this);
        }
    }
}