using System;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Windows.Models
{
    [Serializable]
    public class GameWindowModel
    {
        [SerializeField] private BoolReactiveProperty isStrategyMode = new (false);

        public IReactiveProperty<bool> IsStrategyModeReactive => isStrategyMode;
        
        public bool IsStrategyMode
        {
            get => isStrategyMode.Value;
            set => isStrategyMode.Value = value;
        }
    }
}