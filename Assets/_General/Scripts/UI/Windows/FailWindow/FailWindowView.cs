using _General.Scripts.UI.Windows.BaseWindow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _General.Scripts.UI.Windows.FailWindow
{
    public class FailWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private FailWindowPresenter _presenter;

        [Header("Buttons")]
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        
        public override void Initialize()
        {
            base.Initialize();
            _presenter.HomeCommand.BindTo(_homeButton).AddTo(Disposables);
            _presenter.RestartCommand.BindTo(_restartButton).AddTo(Disposables);
        }
    }
}