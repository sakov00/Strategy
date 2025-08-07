using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Factories;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.SO;
using _Project.Scripts.UI.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private WindowsManager windowsManager;
        
        [Header("Configs")]
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private OthersPrefabConfig othersPrefabConfig;
        [SerializeField] private UnitPrefabConfig unitPrefabConfig;
        [SerializeField] private BuildingPrefabConfig buildingPrefabConfig;
        [SerializeField] private ProjectilePrefabConfig projectilePrefabConfig;
        [SerializeField] private WindowsConfig windowsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(InjectManager.Initialize);
            
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            builder.Register<AppData>(Lifetime.Singleton).AsSelf().As<IInitializable>();
            
            builder.Register<GameManager>(Lifetime.Singleton).As<GameManager, IStartable>();
            builder.Register<ResetService>(Lifetime.Singleton).AsSelf().As<IInitializable>();
            builder.Register<LevelController>(Lifetime.Singleton).AsSelf();
            builder.Register<JsonLoader>(Lifetime.Singleton).AsSelf();
            
            RegisterWindows(builder);
            RegisterRegistries(builder);
            RegisterFactories(builder);
            RegisterSO(builder);
        }
        
        private void RegisterWindows(IContainerBuilder builder)
        {
            builder.RegisterInstance(windowsManager).AsSelf().As<IInitializable>();
            //builder.RegisterComponentInNewPrefab(windowsConfig.gameWindowViewModel, Lifetime.Singleton);
        }
        
        private void RegisterRegistries(IContainerBuilder builder)
        {
            builder.Register<SaveRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<SpawnRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<HealthRegistry>(Lifetime.Singleton).AsSelf();
            builder.Register<TooltipRegistry>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<OthersFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BuildFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<ProjectileFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<FriendFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<EnemyFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<LevelFactory>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterSO(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelsConfig).AsSelf();
            builder.RegisterInstance(othersPrefabConfig).AsSelf();
            builder.RegisterInstance(unitPrefabConfig).AsSelf();
            builder.RegisterInstance(buildingPrefabConfig).AsSelf();
            builder.RegisterInstance(projectilePrefabConfig).AsSelf();
            builder.RegisterInstance(windowsConfig).AsSelf().As<IInitializable>();
        }
    }
}