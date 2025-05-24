using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using Joystick_Pack.Scripts.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Build Prefabs")]
        [SerializeField] private MoneyBuildModel moneyBuildModelPrefab;
        [SerializeField] private TowerDefenceModel towerDefenceModelPrefab;
        [SerializeField] private FriendsBuildModel meleeFriendsBuildModelPrefab;
        [SerializeField] private FriendsBuildModel distanceFriendsBuildModelPrefab;
        
        [Header("Friend Prefabs")]
        [SerializeField] private UnitModel simpleMeleeFriendModelPrefab;
        [SerializeField] private UnitModel simpleDistanceFriendModelPrefab;
        
        [SerializeField] private Joystick joystick;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerController>();

            builder.Register<PlayerInputSystem>(Lifetime.Singleton).As<PlayerInputSystem, ITickable>();
            builder.Register<GameTimer>(Lifetime.Singleton).As<GameTimer, IInitializable, ITickable>();
            
            RegisterFactories(builder);
            
            builder.RegisterComponent(joystick).AsSelf();
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<BuildFactory>(Lifetime.Singleton).AsSelf()
                .WithParameter(nameof(moneyBuildModelPrefab), moneyBuildModelPrefab)
                .WithParameter(nameof(towerDefenceModelPrefab), towerDefenceModelPrefab)
                .WithParameter(nameof(meleeFriendsBuildModelPrefab), meleeFriendsBuildModelPrefab)
                .WithParameter(nameof(distanceFriendsBuildModelPrefab), distanceFriendsBuildModelPrefab);

            // builder.Register<EnemyFactory>(Lifetime.Singleton).AsSelf()
            //     .WithParameter(nameof(simpleMeleeFriendModelPrefab), simpleMeleeFriendModelPrefab)
            //     .WithParameter(nameof(simpleDistanceFriendModelPrefab), simpleDistanceFriendModelPrefab);
               
            builder.Register<FriendFactory>(Lifetime.Singleton).AsSelf()
                .WithParameter(nameof(simpleMeleeFriendModelPrefab), simpleMeleeFriendModelPrefab)
                .WithParameter(nameof(simpleDistanceFriendModelPrefab), simpleDistanceFriendModelPrefab);
        }
    }
}