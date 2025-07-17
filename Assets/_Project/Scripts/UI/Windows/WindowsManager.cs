using System;
using System.Collections.Generic;
using _Project.Scripts._VContainer;
using _Project.Scripts.SO;
using _Project.Scripts.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Windows
{
    public class WindowsManager : MonoBehaviour, IInitializable
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private WindowsConfig _windowsConfig;
        
        private readonly Dictionary<Type, BaseWindowView> _cachedWindows = new ();
        
        public void Initialize()
        {
            InjectManager.Inject(this);
        }
        
        public T GetWindow<T>() where T : BaseWindowView
        {
            return _cachedWindows.TryGetValue(typeof(T), out var window) ? (T)window : null;
        }
        
        public void ShowWindow<T>() where T : BaseWindowView
        {
            if (!_cachedWindows.TryGetValue(typeof(T), out var window))
            {
                window = _resolver.Instantiate(_windowsConfig.Windows[typeof(T)], parent: transform);
                _cachedWindows.Add(typeof(T), window);
            }

            window.Show();
        }
        
        public void HideWindow<T>() where T : BaseWindowView
        {
            if (!_cachedWindows.TryGetValue(typeof(T), out var window))
            {
                window = _resolver.Instantiate(_windowsConfig.Windows[typeof(T)], parent: transform);
                _cachedWindows.Add(typeof(T), window);
            }

            window.Hide();
        }
    }
}