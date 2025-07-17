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

        public IReactiveProperty<bool> IsNextRoundAvailableReactive => isNextRoundAvailable;
        public IReactiveProperty<bool> IsStrategyModeReactive => isStrategyMode;
        
        public bool IsStrategyMode
        {
            get => isStrategyMode.Value;
            set => isStrategyMode.Value = value;
        }
        
        public bool IsNextRoundAvailable
        {
            get => isNextRoundAvailable.Value;
            set => isNextRoundAvailable.Value = value;
        }
    }
}