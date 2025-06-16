using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

namespace _Project.Scripts.Windows.Presenters
{
    public class GameWindowPresenter : BaseWindowPresenter
    {
        [Inject] private ResetService resetService;
        [Inject] private HealthRegistry healthRegistry;
        [Inject] private SpawnRegistry spawnRegistry;
        
        [SerializeField] private GameWindowModel model;
        [SerializeField] private GameWindowView view;
        
        private void Awake()
        {
            healthRegistry.GetAll().ObserveRemove()    
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
            foreach (var spawnPoint in spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }
        
        private void SetNewRound()
        {
            if(healthRegistry.GetAll().Any(x => x.WarSide == WarSide.Enemy))
                return;
            resetService.ResetRound();
            
            model.CurrentRound++;
            view.SetEnableNextRoundButton(true);
        }
    }
}