using System;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Windows.PauseWindow
{
    [Serializable]
    public class PauseWindowModel
    {
        [SerializeField] public BoolReactiveProperty isPaused = new (false);
        
        public IReactiveProperty<bool> IsPausedReactive => isPaused;
        
        public bool IsPaused
        {
            get => isPaused.Value;
            set => isPaused.Value = value;
        }
    }
}