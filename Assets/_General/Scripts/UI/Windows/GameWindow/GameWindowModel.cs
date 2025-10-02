using System;
using _General.Scripts.UI.Windows.BaseWindow;
using UniRx;
using UnityEngine;

namespace _General.Scripts.UI.Windows.GameWindow
{
    [Serializable]
    public class GameWindowModel : BaseWindowModel
    {
        [SerializeField] private BoolReactiveProperty _isStrategyMode = new (false);

        public IReactiveProperty<bool> IsStrategyModeReactive => _isStrategyMode;
        
        public bool IsStrategyMode
        {
            get => _isStrategyMode.Value;
            set => _isStrategyMode.Value = value;
        }
    }
}