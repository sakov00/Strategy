using _General.Scripts.UI.Windows.BaseWindow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _General.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private PauseWindowPresenter _presenter;

        [Header("Buttons")]
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        public override void Initialize()
        {
            base.Initialize();
            _presenter.HomeCommand.BindTo(_homeButton).AddTo(Disposables);
            _presenter.RestartCommand.BindTo(_restartButton).AddTo(Disposables);
            _presenter.ContinueCommand.BindTo(_continueButton).AddTo(Disposables);
        }
    }
}