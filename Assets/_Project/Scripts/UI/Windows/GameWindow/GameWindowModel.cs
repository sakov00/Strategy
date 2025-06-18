using System;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Windows.Models
{
    [Serializable]
    public class GameWindowModel
    {
        [SerializeField] private BoolReactiveProperty isNextRoundAvailable = new (true);
        [SerializeField] private BoolReactiveProperty isStrategyMode = new (false);
        [SerializeField] private IntReactiveProperty currentRound = new (0);
        [SerializeField] private IntReactiveProperty money = new(0);
        public IReactiveProperty<bool> IsNextRoundAvailableReactive => isNextRoundAvailable;
        public IReactiveProperty<bool> IsStrategyModeReactive => isStrategyMode;
        public IReactiveProperty<int> CurrentRoundReactive => currentRound;
        public IReactiveProperty<int> MoneyReactive => money;
        
        public bool IsNextRoundAvailable
        {
            get => isNextRoundAvailable.Value;
            set => isNextRoundAvailable.Value = value;
        }
        
        public bool IsStrategyMode
        {
            get => isStrategyMode.Value;
            set => isStrategyMode.Value = value;
        }

        public int CurrentRound
        {
            get => currentRound.Value;
            set => currentRound.Value = value;
        }
        
        public int Money
        {
            get => money.Value;
            set => money.Value = value;
        }
    }
}