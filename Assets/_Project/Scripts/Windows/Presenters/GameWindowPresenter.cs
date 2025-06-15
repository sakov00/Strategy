using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowPresenter : BaseWindowPresenter
    {
        [SerializeField] private GameWindowModel model;
        [SerializeField] private GameWindowView view;
        
        private void Awake()
        {
            GlobalObjects.GameData.allDamagables.ObserveRemove()    
                .Subscribe(_ => SetNewRound())
                .AddTo(this);
            
            model.MoneyReactive
                .Subscribe(view.SetMoneyText)
                .AddTo(this);
            
            model.CurrentRoundReactive
                .Subscribe(view.SetCurrentRoundText)
                .AddTo(this);
        }

        public Vector3 GetInputVector() =>
            view.GetJoystickInputVector();
        
        public int GetCurrentRound() =>
            model.CurrentRound;

        public void AddMoney(int value) =>
            model.Money += value;
        
        public void NextRoundOnClick()
        {
            view.SetEnableNextRoundButton(false);
            GlobalObjects.GameData.spawnPoints.ForEach(x => x.StartSpawn());
        }
        
        private void SetNewRound()
        {
            if(GlobalObjects.GameData.allDamagables.Any(x => x.WarSide == WarSide.Enemy))
                return;
            
            model.CurrentRound++;
            view.SetEnableNextRoundButton(true);
        }
    }
}