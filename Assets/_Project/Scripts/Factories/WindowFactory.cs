using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.SO;
using _Project.Scripts.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class WindowFactory
    {
        [Inject] private LifetimeScope scope;
        [Inject] private IObjectResolver resolver;
        [Inject] private WindowsConfig windowsConfig;
        
        public T CreateWindow<T>() where T : BaseWindow
        {
            if (typeof(T) == typeof(GameWindow))
            {
                return CreateGameWindow() as T;
            }

            return null;
        }

        private GameWindow CreateGameWindow()
        {
            var gameWindow = resolver.Instantiate(windowsConfig.gameWindow);
            return gameWindow;
        }
    }
}