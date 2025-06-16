using System.Linq;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Models;
using UniRx;
using UnityEngine;
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
            BindHealthRegistry();
            BindModel();
        }

        private void BindHealthRegistry()
        {
            healthRegistry
                .GetAll()
                .ObserveRemove()
                .Subscribe(_ => TryStartNewRound())
                .AddTo(this);
        }

        private void BindModel()
        {
            model.MoneyReactive
                .Subscribe(view.UpdateMoney)
                .AddTo(this);

            model.CurrentRoundReactive
                .Subscribe(view.UpdateCurrentRound)
                .AddTo(this);

            model.CurrentRoundReactive
                .Subscribe(_ => view.SetNextRoundButtonActive(true))
                .AddTo(this);

            model.IsStrategyModeReactive
                .Subscribe(isStrategy =>
                {
                    view.SetJoystickActive(!isStrategy);
                    view.SetTouchAndMouseDragInputActive(isStrategy);
                    GlobalObjects.OneCamera.GetComponent<IsometricCameraFollow>().enabled = !isStrategy;
                })
                .AddTo(this);
        }

        public Vector3 GetInputVector() => view.GetJoystickInputVector();
        public void SetStrategyMode() => model.IsStrategyMode = !model.IsStrategyMode;
        public int GetCurrentRound() => model.CurrentRound;
        public void AddMoney(int amount) => model.Money += amount;

        public void NextRoundOnClick()
        {
            foreach (var spawnPoint in spawnRegistry.GetAll())
            {
                spawnPoint.StartSpawn();
            }
        }

        private void TryStartNewRound()
        {
            bool enemiesRemain = healthRegistry.GetAll().Any(unit => unit.WarSide == WarSide.Enemy);
            if (enemiesRemain) return;

            resetService.ResetRound();
            model.CurrentRound++;
        }
    }
}