using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.SO;
using _Project.Scripts.Windows;
using _Project.Scripts.Windows.Presenters;
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
        
        public T CreateWindow<T>() where T : BaseWindowPresenter
        {
            if (typeof(T) == typeof(GameWindowViewModel))
            {
                return CreateGameWindow() as T;
            }

            return null;
        }

        private GameWindowViewModel CreateGameWindow()
        {
            var gameWindow = resolver.Instantiate(windowsConfig.gameWindowViewModel);
            return gameWindow;
        }
    }
}