using System;
using _General.Scripts.Enums;
using UniRx;
using UnityEngine;

namespace _General.Scripts.UI.Windows
{
    [Serializable]
    public abstract class BaseWindowModel
    {
        [SerializeField] protected ReactiveProperty<WindowType> _windowType = new (WindowType.Window);
        [SerializeField] protected BoolReactiveProperty _isPaused = new (false);
        
        public virtual IReactiveProperty<WindowType> WindowTypeReactive => _windowType;
        public virtual IReactiveProperty<bool> IsPausedReactive => _isPaused;
        
        public virtual WindowType WindowType
        {
            get => _windowType.Value;
            set => _windowType.Value = value;
        }
        
        public virtual bool IsPaused
        {
            get => _isPaused.Value;
            set => _isPaused.Value = value;
        }
    }
}