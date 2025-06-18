using _Project.Scripts._GlobalLogic;
using _Project.Scripts.UI.Elements;
using _Project.Scripts.Windows;
using _Project.Scripts.Windows.Presenters;
using Joystick_Pack.Scripts.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows.GameWindow
{
    public class GameWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private GameWindowViewModel viewModel;

        [Header("Buttons")]
        [SerializeField] private Button nextRoundButton;
        [SerializeField] private Button strategyModeButton;

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI currentRoundText;

        [Header("Controls")]
        [SerializeField] private Joystick joystick;
        [SerializeField] private TouchAndMouseDragInput touchAndMouseDragInput;
        
        private const string MoneyFormat = "Money: {0}";
        private const string RoundFormat = "Round: {0}";

        private void Start()
        {
            viewModel.NextRoundCommand.BindTo(nextRoundButton).AddTo(this);
            viewModel.IsNextRoundAvailable
                .Subscribe(isVisible => nextRoundButton.gameObject.SetActive(isVisible))
                .AddTo(this);
            
            viewModel.SetStrategyModeCommand.BindTo(strategyModeButton).AddTo(this);
            viewModel.IsStrategyMode
                .Subscribe(isStrategy =>
                {
                    joystick.gameObject.SetActive(!isStrategy);
                    touchAndMouseDragInput.gameObject.SetActive(isStrategy);
                    GlobalObjects.CameraController.cameraFollow.IsFollowing = !isStrategy;
                })
                .AddTo(this);
            
            viewModel.Money
                .Subscribe(money => moneyText.text = string.Format(MoneyFormat, money))
                .AddTo(this);
            
            viewModel.CurrentRound
                .Subscribe(roundIndex => currentRoundText.text = string.Format(RoundFormat, roundIndex + 1))
                .AddTo(this);
        }

        public void Update()
        {
            var direction = joystick.Direction;
            
            if(!Mathf.Approximately(direction.x, viewModel.MoveDirection.x) && !Mathf.Approximately(direction.y, viewModel.MoveDirection.z))
                viewModel.MoveDirection = new Vector3(direction.x, 0f, direction.y);
        }
    }
}