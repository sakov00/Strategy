using _Project.Scripts._VContainer;
using _Redactor.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts._VContainer
{
    public class RedactorLifetimeScope : GameLifetimeScope
    {
        [Header("Redactor Settings")]
        [SerializeField] private RedactorManager _redactorManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(_redactorManager).AsSelf();
            builder.Register<SaveLoadLevelService>(Lifetime.Singleton).AsSelf();
        }
    }
}