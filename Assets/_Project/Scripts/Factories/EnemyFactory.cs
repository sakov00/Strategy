using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
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
        
        public UnitModel CreateEnemyUnit(EnemyUnitType unitType, Vector3 position, Vector3 noAimPosition, Quaternion rotation = default)
        {
            UnitModel enemyModel;
            switch (unitType)
            {
                case EnemyUnitType.SimpleMelee:
                    enemyModel = CreateMoneyBuilding(position, rotation);
                    break;
                case EnemyUnitType.SimpleDistance:
                    enemyModel = CreateTowerDefenseBuilding(position, rotation);
                    break;
                default: return null;
            }
            
            enemyModel.NoAimPosition = noAimPosition;
            return enemyModel;
        }

        private UnitModel CreateMoneyBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.meleeEnemyPrefab, position, rotation);
        }
        
        private UnitModel CreateTowerDefenseBuilding(Vector3 position, Quaternion rotation)
        {
            return resolver.Instantiate(unitPrefabConfig.distanceEnemyPrefab, position, rotation);
        }
    }
}