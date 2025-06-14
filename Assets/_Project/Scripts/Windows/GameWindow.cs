using System;
using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.SpawnPoints;
using Joystick_Pack.Scripts.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Windows
{
    public class GameWindow : BaseWindow
    {
        [SerializeField] public Joystick joystick;
        
        [SerializeField] private Button nextRoundButton;
        
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI currentRoundText;
        
        private void Awake()
        {
            nextRoundButton
                .OnClickAsObservable()
                .Subscribe(_ => NextRound())
                .AddTo(this);
            
            GlobalObjects.GameData.levelData.MoneyReactive
                .Subscribe(value => moneyText.text = $"Money: {value}")
                .AddTo(this);
            
            GlobalObjects.GameData.levelData.CurrentRoundReactive
                .Subscribe(value => currentRoundText.text = $"Round: {value}")
                .AddTo(this);
        }

        private void NextRound()
        {
            GlobalObjects.GameData.spawnPoints.ForEach(x => x.StartSpawn());
            GlobalObjects.GameData.levelData.CurrentRound++;
        }
    }
}