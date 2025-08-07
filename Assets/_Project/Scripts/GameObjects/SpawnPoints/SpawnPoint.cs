using System;
using System.Collections.Generic;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Registries;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameObjects.SpawnPoints
{
    [Serializable]
    [RequireComponent(typeof(SplineContainer))]
    public class SpawnPoint : MonoBehaviour
    {
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private SpawnRegistry _spawnRegistry;
        [Inject] private SaveRegistry _saveRegistry;

        [SerializeField] private SplineContainer _splineContainer = new();
        [SerializeField] private List<EnemyGroup> roundEnemyList = new();

        private List<Vector3> _worldPositions = new();
        private List<EnemyWithTime> _currentEnemyList;
        private int _currentIndex;
        private float _elapsedTime;

        private void OnValidate()
        {
            _splineContainer ??= GetComponent<SplineContainer>();
        }

        private void Start()
        {
            InjectManager.Inject(this);
            _spawnRegistry.Register(this);

            foreach (var knot in _splineContainer.Spline)
            {
                Vector3 localPosition = knot.Position;
                Vector3 worldPosition = _splineContainer.transform.TransformPoint(localPosition);
                _worldPositions.Add(worldPosition);
            }
        }

        public void StartSpawn()
        {
            int currentRound = AppData.LevelData.CurrentRound;
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
            var offsetX = Random.Range(-5f, 5f);
            var wayPoints = new List<Vector3>();
            foreach (var position in _worldPositions)
            {
                wayPoints.Add(position + new Vector3(offsetX, 0f, 0f));
            }
            _enemyFactory.CreateEnemyUnit(enemyData.enemyType, wayPoints[0], wayPoints);
        }

        private void OnDestroy()
        {
            GameTimer.Instance.OnEverySecond -= Tick;
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
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
