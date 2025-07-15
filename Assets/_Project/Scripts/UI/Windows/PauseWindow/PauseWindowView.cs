using _Project.Scripts.Windows;
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
        [SerializeField] private Button restartModeButton;
        [SerializeField] private Button continueButton;

        private void Start()
        {
            viewModel.HomeCommand.BindTo(closeButton).AddTo(this);
            viewModel.RestartCommand.BindTo(restartModeButton).AddTo(this);
            viewModel.ContinueCommand.BindTo(continueButton).AddTo(this);
        }
    }
}