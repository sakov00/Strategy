using _General.Scripts.UI.Windows.BaseWindow;
using DG.Tweening;
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
        
        public override Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => Time.timeScale = 0);
            sequence.Append(base.Show());
            sequence.SetUpdate(true);
            return sequence;
        }

        public override Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(base.Hide());
            sequence.AppendCallback(() => Time.timeScale = 1);
            sequence.SetUpdate(true);
            return sequence;
        }
        
        public override void ShowFast()
        {
            Time.timeScale = 0;
            base.ShowFast();
        }

        public override void HideFast()
        {
            Time.timeScale = 1;
            base.HideFast();
        }
    }
}