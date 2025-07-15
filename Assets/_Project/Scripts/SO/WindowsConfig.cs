using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.Windows;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.SO
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "SO/Windows Config")]
    public class WindowsConfig : ScriptableObject, IInitializable
    {
        [SerializeField] private List<BaseWindowView> windowsList = new();
        
        public Dictionary<Type, BaseWindowView> Windows;
        
        public void Initialize()
        {
            Windows = windowsList.ToDictionary(w => w.GetType(), w => w);
        }
    }
}