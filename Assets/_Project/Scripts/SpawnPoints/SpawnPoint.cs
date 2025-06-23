using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.SpawnPoints
{
    public class SpawnPoint : MonoBehaviour
    {
        [Inject] private GameWindowViewModel gameWindowViewModel;
        [Inject] private EnemyFactory enemyFactory;
        [Inject] private SpawnRegistry spawnRegistry;
        
        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private List<EnemyGroup> roundEnemyList = new();
        
        private Queue<EnemyUnitType> spawnQueue;
        
        private int spawnedMelee;
        private int spawnedDistance;

        private void Start()
        {
            InjectManager.Inject(this);
            spawnRegistry.Register(this);
        }

        public void StartSpawn()
        {
            //spawnQueue = new Queue<EnemyUnitType>(roundEnemyList[gameWindowViewModel.GetCurrentRound()].enemies);
            
            GameTimer.Instance.OnEverySecond -= SpawnEnemy;
            GameTimer.Instance.OnEverySecond += SpawnEnemy;
        }

        private void SpawnEnemy()
        {
            if (spawnQueue.Count == 0)
            {
                GameTimer.Instance.OnEverySecond -= SpawnEnemy;
                return;
            }
            
            var randomOffset2D = UnityEngine.Random.insideUnitCircle * spawnRadius;
            var spawnOffset = new Vector3(randomOffset2D.x, 0, randomOffset2D.y);
            var spawnPosition = transform.position + spawnOffset;
            
            var nextEnemyType = spawnQueue.Dequeue();
            enemyFactory.CreateEnemyUnit(nextEnemyType, spawnPosition, new Vector3(58, 0, 94));
        }
        
        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= SpawnEnemy;
        }
    }
    
    [System.Serializable]
    public class EnemyGroup
    {
        public List<EnemyWithTime> enemies;
    }
    
    [System.Serializable]
    public class EnemyWithTime
    {
        public float time;
        public EnemyUnitType enemyType;
        
    }
}