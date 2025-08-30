using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _General.Scripts.SO;
using _General.Scripts.UI.Windows;
using _Project.Scripts.Factories;
using _Project.Scripts.Pools;
using _Project.Scripts.Services;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private WindowsManager _windowsManager;
        [SerializeField] private GameManager _gameManager;
        
        [Header("Configs")]
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private OthersPrefabConfig _othersPrefabConfig;
        [SerializeField] private CharacterPrefabConfig _characterPrefabConfig;
        [SerializeField] private BuildingPrefabConfig _buildingPrefabConfig;
        [SerializeField] private ProjectilePrefabConfig _projectilePrefabConfig;
        [SerializeField] private WindowsConfig _windowsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(InjectManager.Initialize);
            
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            builder.Register<AppData>(Lifetime.Singleton).AsSelf().As<IInitializable>();
            
            builder.RegisterInstance(_gameManager).AsSelf();
            builder.Register<ResetLevelService>(Lifetime.Singleton).AsSelf().As<IInitializable>();
            builder.Register<LevelSaveLoadService>(Lifetime.Singleton).AsSelf();
            builder.Register<JsonLoader>(Lifetime.Singleton).AsSelf();
            
            RegisterWindows(builder);
            RegisterRegistries(builder);
            RegisterPools(builder);
            RegisterFactories(builder);
            RegisterSO(builder);
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
            builder.Register<LevelFactory>(Lifetime.Singleton).AsSelf();
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
    }
}