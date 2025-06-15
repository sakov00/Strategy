using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.SpawnPoints;
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
        [SerializeField] private GameWindowPresenter presenter;
        
        [SerializeField] private Button nextRoundButton;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI currentRoundText;
        [SerializeField] private Joystick joystick;

        private void Awake()
        {
            nextRoundButton
                .OnClickAsObservable()
                .Subscribe(_ => presenter.NextRoundOnClick())
                .AddTo(this);
        }

        public Vector3 GetJoystickInputVector() => 
            new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        
        public void SetEnableNextRoundButton(bool value)
        {
            nextRoundButton.gameObject.SetActive(value);
        }
        
        public void SetMoneyText(int value)
        {
            moneyText.text = $"Money: {value}";
        }
        
        public void SetCurrentRoundText(int value)
        {
            currentRoundText.text = $"Round: {value + 1}";
        }
    }
}