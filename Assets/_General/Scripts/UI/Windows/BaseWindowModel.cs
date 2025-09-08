using System;
using _General.Scripts.Enums;
using UniRx;
using UnityEngine;

namespace _General.Scripts.UI.Windows
{
    [Serializable]
    public abstract class BaseWindowModel
    {
        [SerializeField] protected WindowType _windowType = WindowType.Window;
        [SerializeField] protected BoolReactiveProperty _isPaused = new (false);
        
        public virtual WindowType WindowType => _windowType;
        public virtual IReactiveProperty<bool> IsPausedReactive => _isPaused;
        
        public virtual bool IsPaused
        {
            get => _isPaused.Value;
            set => _isPaused.Value = value;
        }
    }
}