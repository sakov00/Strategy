using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
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
                .Subscribe(_ => NextRoundOnClick())
                .AddTo(this);
            
            GlobalObjects.GameData.levelData.MoneyReactive
                .Subscribe(value => moneyText.text = $"Money: {value}")
                .AddTo(this);
            
            GlobalObjects.GameData.levelData.CurrentRoundReactive
                .Subscribe(value => currentRoundText.text = $"Round: {value + 1}")
                .AddTo(this);
            
            GlobalObjects.GameData.allDamagables.ObserveRemove()    
                .Subscribe(_ => SetNewRound())
                .AddTo(this);
        }

        private void NextRoundOnClick()
        {
            nextRoundButton.gameObject.SetActive(false);
            GlobalObjects.GameData.spawnPoints.ForEach(x => x.StartSpawn());
        }
        
        private void SetNewRound()
        {
            if(GlobalObjects.GameData.allDamagables.Any(x => x.WarSide == WarSide.Enemy))
                return;
            
            GlobalObjects.GameData.levelData.CurrentRound++;
            nextRoundButton.gameObject.SetActive(true);
        }
    }
}