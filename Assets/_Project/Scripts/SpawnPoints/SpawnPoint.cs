using System;
using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Scripts.SpawnPoints
{
    [Serializable]
    public class SpawnPoint : MonoBehaviour
    {
        [Inject] private GameWindowViewModel gameWindowViewModel;
        [Inject] private EnemyFactory enemyFactory;
        [Inject] private SpawnRegistry spawnRegistry;

        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private List<EnemyGroup> roundEnemyList = new();

        private List<EnemyWithTime> currentEnemyList;
        private int currentIndex;
        private float elapsedTime;

        private void Start()
        {
            InjectManager.Inject(this);
            spawnRegistry.Register(this);
        }
        
        public SpawnDataJson GetJsonData()
        {
            var spawnDataJson = new SpawnDataJson();
            spawnDataJson.position = transform.position;
            spawnDataJson.spawnRadius = spawnRadius;
            spawnDataJson.roundEnemyList.AddRange(roundEnemyList);
            return spawnDataJson;
        }

        public SpawnDataJson SetJsonData(SpawnDataJson spawnDataJson)
        {
            transform.position = spawnDataJson.position;
            spawnRadius = spawnDataJson.spawnRadius;
            roundEnemyList.AddRange(spawnDataJson.roundEnemyList);
            return spawnDataJson;
        }

        public void StartSpawn()
        {
            int currentRound = gameWindowViewModel.CurrentRound.Value;
            if (currentRound >= roundEnemyList.Count)
            {
                Debug.LogWarning("Нет настроек для текущего раунда спавна.");
                return;
            }

            currentEnemyList = roundEnemyList[currentRound].enemies;
            currentEnemyList.Sort((a, b) => a.time.CompareTo(b.time)); // сортировка на всякий случай
            currentIndex = 0;
            elapsedTime = 0f;

            GameTimer.Instance.OnEverySecond -= Tick;
            GameTimer.Instance.OnEverySecond += Tick;
        }

        private void Tick()
        {
            elapsedTime += 1f;

            while (currentIndex < currentEnemyList.Count && elapsedTime >= currentEnemyList[currentIndex].time)
            {
                Spawn(currentEnemyList[currentIndex]);
                currentIndex++;
            }

            if (currentIndex >= currentEnemyList.Count)
            {
                GameTimer.Instance.OnEverySecond -= Tick;
            }
        }

        private void Spawn(EnemyWithTime enemyData)
        {
            var offset2D = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = transform.position + new Vector3(offset2D.x, 0, offset2D.y);

            enemyFactory.CreateEnemyUnit(enemyData.enemyType, spawnPosition, new Vector3(58, 0, 94));
        }

        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= Tick;
        }
    }

    [Serializable]
    public class EnemyGroup
    {
        public List<EnemyWithTime> enemies;
    }

    [Serializable]
    public class EnemyWithTime
    {
        [Min(0f)]
        public float time;
        public EnemyUnitType enemyType;
    }
}
