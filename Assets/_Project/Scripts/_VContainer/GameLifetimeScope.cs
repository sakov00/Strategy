using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.SO;
using _General.Scripts.UI.Windows;
using _Project.Scripts.Factories;
using _Project.Scripts.Pools;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] protected WindowsManager _windowsManager;
        [SerializeField] protected PoolsManager _poolsManager;
        
        [Header("Configs")]
        [SerializeField] protected LevelsConfig _levelsConfig;
        [SerializeField] protected OthersPrefabConfig _othersPrefabConfig;
        [SerializeField] protected CharacterPrefabConfig _characterPrefabConfig;
        [SerializeField] protected BuildingPrefabConfig _buildingPrefabConfig;
        [SerializeField] protected ProjectilePrefabConfig _projectilePrefabConfig;
        [SerializeField] protected WindowsConfig _windowsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(InjectManager.Initialize);
            
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            
            RegisterGameManager(builder);
            RegisterAppData(builder);
            RegisterWindows(builder);
            RegisterRegistries(builder);
            RegisterPools(builder);
            RegisterFactories(builder);
            RegisterSO(builder);
            RegisterServices(builder);
        }

        public virtual void RegisterGameManager(IContainerBuilder builder)
        {
            builder.Register<GameManager>(Lifetime.Singleton).AsSelf().As<IStartable>();
        }
        
        private void RegisterAppData(IContainerBuilder builder)
        {
            builder.Register<AppData>(Lifetime.Singleton).AsSelf().As<IInitializable>();
        }
        
        private void RegisterWindows(IContainerBuilder builder)
        {
            builder.RegisterInstance(_windowsManager).AsSelf().As<IInitializable>();
            //builder.RegisterComponentInNewPrefab(windowsConfig.gameWindowViewModel, Lifetime.Singleton);
        }
        
        private void RegisterRegistries(IContainerBuilder builder)
        {
            builder.Register<ObjectsRegistry>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterPools(IContainerBuilder builder)
        {
            builder.Register<BuildPool>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterPool>(Lifetime.Singleton).AsSelf();
            builder.Register<ProjectilePool>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<OthersFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BuildFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<ProjectileFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<EnvironmentFactory>(Lifetime.Singleton).AsSelf();
        }
        
        private void RegisterSO(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelsConfig).AsSelf();
            builder.RegisterInstance(_othersPrefabConfig).AsSelf();
            builder.RegisterInstance(_characterPrefabConfig).AsSelf();
            builder.RegisterInstance(_buildingPrefabConfig).AsSelf();
            builder.RegisterInstance(_projectilePrefabConfig).AsSelf();
            builder.RegisterInstance(_windowsConfig).AsSelf().As<IInitializable>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.RegisterInstance(_poolsManager).AsSelf();
            builder.Register<ResetLevelService>(Lifetime.Singleton).AsSelf();
            builder.Register<SaveLoadLevelService>(Lifetime.Singleton).AsSelf();
        }
    }
}