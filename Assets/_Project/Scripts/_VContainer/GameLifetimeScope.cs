using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects._Controllers;
using _Project.Scripts.GameObjects.Characters.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Joystick joystick;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerController>();

            builder.Register<PlayerInputSystem>(Lifetime.Singleton).As<PlayerInputSystem, ITickable>();
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            builder.RegisterComponent(joystick).AsSelf();
        }
    }
}