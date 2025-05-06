using _Project.Scripts.Controllers;
using _Project.Scripts.Systems;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerController>();

            builder.Register<PlayerInputSystem>(Lifetime.Singleton).As<PlayerInputSystem, ITickable>();
        }
    }
}