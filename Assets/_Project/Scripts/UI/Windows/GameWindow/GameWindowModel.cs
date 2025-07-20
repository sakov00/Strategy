using System;
using _Project.Scripts.UI.Windows;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Windows.Models
{
    [Serializable]
    public class GameWindowModel : BaseWindowModel
    {
        [SerializeField] private BoolReactiveProperty _isNextRoundAvailable = new (true);
        [SerializeField] private BoolReactiveProperty _isStrategyMode = new (false);

        public IReactiveProperty<bool> IsNextRoundAvailableReactive => _isNextRoundAvailable;
        public IReactiveProperty<bool> IsStrategyModeReactive => _isStrategyMode;
        
        public bool IsStrategyMode
        {
            get => _isStrategyMode.Value;
            set => _isStrategyMode.Value = value;
        }
        
        public bool IsNextRoundAvailable
        {
            get => _isNextRoundAvailable.Value;
            set => _isNextRoundAvailable.Value = value;
        }
    }
}