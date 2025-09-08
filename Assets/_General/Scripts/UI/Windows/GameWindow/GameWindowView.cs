using _General.Scripts._GlobalLogic;
using _General.Scripts.AllAppData;
using _General.Scripts.UI.Elements;
using DG.Tweening;
using Joystick_Pack.Scripts.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _General.Scripts.UI.Windows.GameWindow
{
    public class GameWindowView : BaseWindowView
    {
        [Inject] private AppData _appData;
        
        [Header("Presenter")]
        [SerializeField] private GameWindowPresenter _presenter;

        [Header("Buttons")]
        [SerializeField] private Button _openPauseMenuButton;
        [SerializeField] private Button _nextRoundButton;
        [SerializeField] private Button _strategyModeButton;

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _currentRoundText;

        [Header("Controls")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private TouchAndMouseDragInput _touchAndMouseDragInput;
        
        protected override BaseWindowPresenter BasePresenter => _presenter;
        
        private const string MoneyFormat = "Money: {0}";
        private const string RoundFormat = "Round: {0}";

        private void Start()
        {
            _presenter.OpenPauseWindowCommand.BindTo(_openPauseMenuButton).AddTo(this);
            _presenter.NextRoundCommand.BindTo(_nextRoundButton).AddTo(this);
            _presenter.IsNextRoundAvailable
                .Subscribe(isVisible => _nextRoundButton.gameObject.SetActive(isVisible))
                .AddTo(this);
            
            _presenter.SetStrategyModeCommand.BindTo(_strategyModeButton).AddTo(this);
            _presenter.IsStrategyMode
                .Subscribe(isStrategy =>
                {
                    _joystick.gameObject.SetActive(!isStrategy);
                    _touchAndMouseDragInput.gameObject.SetActive(isStrategy);
                    GlobalObjects.CameraController.CameraFollow.IsFollowing = !isStrategy;
                })
                .AddTo(this);
            
            _appData.LevelData.MoneyReactive
                .Subscribe(money => _moneyText.text = string.Format(MoneyFormat, money))
                .AddTo(this);
            
            _appData.LevelData.CurrentRoundReactive
                .Subscribe(roundIndex => _currentRoundText.text = string.Format(RoundFormat, roundIndex + 1))
                .AddTo(this);
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
            
            if(!Mathf.Approximately(direction.x, _appData.LevelData.MoveDirection.x) && !Mathf.Approximately(direction.y, _appData.LevelData.MoveDirection.z))
                _appData.LevelData.MoveDirection = new Vector3(direction.x, 0f, direction.y);
        }

        public void Reset()
        {
            _presenter.Reset();
        }
    }
}