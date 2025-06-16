using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.SpawnPoints;
using _Project.Scripts.UI.Elements;
using _Project.Scripts.Windows.Presenters;
using Joystick_Pack.Scripts.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Windows
{
    public class GameWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private GameWindowPresenter presenter;

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
            BindUI();
        }

        private void BindUI()
        {
            nextRoundButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    SetNextRoundButtonActive(false);
                    presenter.NextRoundOnClick();
                })
                .AddTo(this);

            strategyModeButton
                .OnClickAsObservable()
                .Subscribe(_ => presenter.SetStrategyMode())
                .AddTo(this);
        }

        public Vector3 GetJoystickInputVector()
        {
            var direction = joystick.Direction;
            return new Vector3(direction.x, 0f, direction.y);
        }

        public void SetNextRoundButtonActive(bool isActive)
        {
            nextRoundButton.gameObject.SetActive(isActive);
        }

        public void SetJoystickActive(bool isActive)
        {
            joystick.gameObject.SetActive(isActive);
        }
        
        public void SetTouchAndMouseDragInputActive(bool isActive)
        {
            touchAndMouseDragInput.gameObject.SetActive(isActive);
        }

        public void UpdateMoney(int value)
        {
            moneyText.text = string.Format(MoneyFormat, value);
        }

        public void UpdateCurrentRound(int roundIndex)
        {
            currentRoundText.text = string.Format(RoundFormat, roundIndex + 1);
        }
    }
}