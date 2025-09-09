using _Project.Scripts._VContainer;
using _Redactor.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts._VContainer
{
    public class RedactorLifetimeScope : GameLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }
}