using _Project.Scripts;
using _Project.Scripts._VContainer;
using _Redactor.Scripts.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Redactor.Scripts._VContainer
{
    public class RedactorLifetimeScope : GameLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
        
        public override void RegisterGameManager(IContainerBuilder builder)
        {
            builder.Register<RedactorManager>(Lifetime.Singleton).AsSelf().As<IStartable, GameManager>();
        }
    }
}