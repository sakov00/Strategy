using System;
using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
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
    public class SpawnPoint : MonoBehaviour, ISavable<SpawnDataJson>
    {
        [Inject] private GameWindowViewModel _gameWindowViewModel;
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private SaveRegistry _saveRegistry;

        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private List<EnemyGroup> roundEnemyList = new();

        private List<EnemyWithTime> _currentEnemyList;
        private int _currentIndex;
        private float _elapsedTime;

        private void Start()
        {
            InjectManager.Inject(this);
            _saveRegistry.Register(this);
            _spawnRegistry.Register(this);
        }
        
        public SpawnDataJson GetJsonData()
        {
            var spawnDataJson = new SpawnDataJson();
            spawnDataJson.position = transform.position;
            spawnDataJson.spawnRadius = spawnRadius;
            spawnDataJson.roundEnemyList.AddRange(roundEnemyList);
            return spawnDataJson;
        }

        public void SetJsonData(SpawnDataJson spawnDataJson)
        {
            transform.position = spawnDataJson.position;
            spawnRadius = spawnDataJson.spawnRadius;
            roundEnemyList.AddRange(spawnDataJson.roundEnemyList);
        }

        public void StartSpawn()
        {
            int currentRound = _gameWindowViewModel.CurrentRound.Value;
            if (currentRound >= roundEnemyList.Count)
            {
                Debug.LogWarning("Нет настроек для текущего раунда спавна.");
                return;
            }

            _currentEnemyList = roundEnemyList[currentRound].enemies;
            _currentEnemyList.Sort((a, b) => a.time.CompareTo(b.time)); // сортировка на всякий случай
            _currentIndex = 0;
            _elapsedTime = 0f;

            GameTimer.Instance.OnEverySecond -= Tick;
            GameTimer.Instance.OnEverySecond += Tick;
        }

        private void Tick()
        {
            _elapsedTime += 1f;

            while (_currentIndex < _currentEnemyList.Count && _elapsedTime >= _currentEnemyList[_currentIndex].time)
            {
                Spawn(_currentEnemyList[_currentIndex]);
                _currentIndex++;
            }

            if (_currentIndex >= _currentEnemyList.Count)
            {
                GameTimer.Instance.OnEverySecond -= Tick;
            }
        }

        private void Spawn(EnemyWithTime enemyData)
        {
            var offset2D = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = transform.position + new Vector3(offset2D.x, 0, offset2D.y);

            _enemyFactory.CreateEnemyUnit(enemyData.enemyType, spawnPosition, new Vector3(58, 0, 94));
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
        public UnitType enemyType;
    }
}
