using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
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
        
        public UnitController CreateEnemyUnit(EnemyUnitType unitType, Vector3 position, Vector3 noAimPosition, Quaternion rotation = default)
        {
            UnitController enemyController;
            switch (unitType)
            {
                case EnemyUnitType.SimpleMelee:
                    enemyController = CreateMoneyBuilding(position, rotation);
                    break;
                case EnemyUnitType.SimpleDistance:
                    enemyController = CreateTowerDefenseBuilding(position, rotation);
                    break;
                default: return null;
            }
            
            enemyController.SetNoAimPosition(noAimPosition);
            healthRegistry.Register(enemyController.Model);
            return enemyController;
        }

        private UnitController CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeEnemyPrefab, position, rotation);
        }
        
        private UnitController CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceEnemyPrefab, position, rotation);
        }
    }
}