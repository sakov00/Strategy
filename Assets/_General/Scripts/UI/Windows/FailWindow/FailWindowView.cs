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

        protected override BaseWindowPresenter BasePresenter => _presenter;
        
        private void Start()
        {
            _presenter.HomeCommand.BindTo(_homeButton).AddTo(this);
            _presenter.RestartCommand.BindTo(_restartButton).AddTo(this);
        }
    }
}