using _Project.Scripts.Windows;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows.PauseWindow
{
    public class PauseWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private PauseWindowViewModel viewModel;

        [Header("Buttons")]
        [SerializeField] private Button homeButton;
        [SerializeField] private Button restartModeButton;

        private void Start()
        {
            viewModel.HomeCommand.BindTo(homeButton).AddTo(this);
            viewModel.RestartCommand.BindTo(restartModeButton).AddTo(this);
            viewModel.ContinueCommand.BindTo(closeButton).AddTo(this);
        }
        
        public override Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => viewModel.IsPaused.Value = true);
            sequence.Append(base.Show());
            sequence.SetUpdate(true);
            return sequence;
        }

        public override Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(base.Hide());
            sequence.AppendCallback(() => viewModel.IsPaused.Value = false);
            sequence.SetUpdate(true);
            return sequence;
        }
    }
}