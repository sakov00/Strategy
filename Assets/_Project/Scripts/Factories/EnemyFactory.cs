using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class EnemyFactory
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private UnitPrefabConfig unitPrefabConfig;
        [Inject] private HealthRegistry healthRegistry;
        
        public UnitController CreateEnemyUnit(UnitType unitType, Vector3 position, Vector3 noAimPosition, Quaternion rotation = default)
        {
            UnitController enemyController;
            switch (unitType)
            {
                case UnitType.SimpleMelee:
                    enemyController = CreateMelee(position, rotation);
                    break;
                case UnitType.SimpleDistance:
                    enemyController = CreateDistance(position, rotation);
                    break;
                default: return null;
            }
            
            enemyController.SetNoAimPosition(noAimPosition);
            healthRegistry.Register(enemyController.ObjectModel);
            return enemyController;
        }

        private UnitController CreateMelee(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeEnemyPrefab, position, rotation);
        }
        
        private UnitController CreateDistance(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceEnemyPrefab, position, rotation);
        }
    }
}