using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using _Project.Scripts.Services;
using _Project.Scripts.SO;
using _Project.Scripts.Windows.Presenters;
using Joystick_Pack.Scripts.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Configs")]
        [SerializeField] private UnitPrefabConfig unitPrefabConfig;
        [SerializeField] private BuildingPrefabConfig buildingPrefabConfig;
        [SerializeField] private WindowsConfig windowsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(InjectManager.Initialize);
            
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            
            builder.Register<InitializeGame>(Lifetime.Singleton).As<InitializeGame, IStartable>();
            builder.Register<ResetController>(Lifetime.Singleton).AsSelf();
            
            RegisterWindows(builder);
            RegisterFactories(builder);
            RegisterSO(builder);
        }
        
        private void RegisterWindows(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(windowsConfig.GameWindowPresenter, Lifetime.Singleton);
        }
        
        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<WindowFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BuildFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<FriendFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<EnemyFactory>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterSO(IContainerBuilder builder)
        {
            builder.RegisterInstance(unitPrefabConfig).AsSelf();
            builder.RegisterInstance(buildingPrefabConfig).AsSelf();
            builder.RegisterInstance(windowsConfig).AsSelf();
        }
    }
}