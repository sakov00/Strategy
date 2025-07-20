using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.UI.Elements;
using _Project.Scripts.Windows;
using DG.Tweening;
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
        [SerializeField] private GameWindowViewModel _viewModel;

        [Header("Buttons")]
        [SerializeField] private Button _openPauseMenuButton;
        [SerializeField] private Button _nextRoundButton;
        [SerializeField] private Button _strategyModeButton;
        
        [Header("Dev")]
        [SerializeField] private Button _fastFailButton;
        [SerializeField] private Button _fastWinButton;
        [SerializeField] private Button _saveLevelButton;
        [SerializeField] private Button _loadLevelButton;
        [SerializeField] private TMP_InputField _selectLevelInputField;

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _currentRoundText;

        [Header("Controls")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private TouchAndMouseDragInput _touchAndMouseDragInput;
        
        protected override BaseWindowViewModel BaseViewModel => _viewModel;
        
        private const string MoneyFormat = "Money: {0}";
        private const string RoundFormat = "Round: {0}";

        private void Start()
        {
            _viewModel.OpenPauseWindowCommand.BindTo(_openPauseMenuButton).AddTo(this);
            _viewModel.NextRoundCommand.BindTo(_nextRoundButton).AddTo(this);
            _viewModel.IsNextRoundAvailable
                .Subscribe(isVisible => _nextRoundButton.gameObject.SetActive(isVisible))
                .AddTo(this);
            
            _viewModel.SetStrategyModeCommand.BindTo(_strategyModeButton).AddTo(this);
            _viewModel.IsStrategyMode
                .Subscribe(isStrategy =>
                {
                    _joystick.gameObject.SetActive(!isStrategy);
                    _touchAndMouseDragInput.gameObject.SetActive(isStrategy);
                    GlobalObjects.CameraController.cameraFollow.IsFollowing = !isStrategy;
                })
                .AddTo(this);
            
            AppData.User.MoneyReactive
                .Subscribe(money => _moneyText.text = string.Format(MoneyFormat, money))
                .AddTo(this);
            
            AppData.LevelData.CurrentRoundReactive
                .Subscribe(roundIndex => _currentRoundText.text = string.Format(RoundFormat, roundIndex + 1))
                .AddTo(this);

#if EDIT_MODE
            _viewModel.FastFailCommand.BindTo(_fastFailButton).AddTo(this);
            _viewModel.FastWinCommand.BindTo(_fastWinButton).AddTo(this);
            _saveLevelButton.onClick.AddListener(() => _viewModel.SaveLevelCommand.Execute(int.Parse(_selectLevelInputField.text)));
            _loadLevelButton.onClick.AddListener(() => _viewModel.LoadLevelCommand.Execute(int.Parse(_selectLevelInputField.text)));
#endif
        }
        
        public override Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => gameObject.SetActive(true));
            sequence.Append(_canvasGroup.DOFade(1f, 0.5f).From(0));
            return sequence;
        }

        public override Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(0f, 0.5f).From(1));
            sequence.AppendCallback(() => gameObject.SetActive(false));
            return sequence;
        }

        public void Update()
        {
            var direction = _joystick.Direction;
            
            if(!Mathf.Approximately(direction.x, AppData.LevelData.MoveDirection.x) && !Mathf.Approximately(direction.y, AppData.LevelData.MoveDirection.z))
                AppData.LevelData.MoveDirection = new Vector3(direction.x, 0f, direction.y);
        }

        public void Reset()
        {
            _viewModel.Reset();
        }
    }
}