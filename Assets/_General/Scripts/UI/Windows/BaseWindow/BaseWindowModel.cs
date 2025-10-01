using System;
using _General.Scripts.Enums;
using UnityEngine;

namespace _General.Scripts.UI.Windows.BaseWindow
{
    [Serializable]
    public class BaseWindowModel
    {
        [SerializeField] protected WindowType _windowType = WindowType.Window;
        
        public WindowType WindowType => _windowType;
    }
}