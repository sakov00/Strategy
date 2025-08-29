using System;
using UniRx;
using UnityEngine;

namespace _General.Scripts.UI.Windows.GameWindow
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