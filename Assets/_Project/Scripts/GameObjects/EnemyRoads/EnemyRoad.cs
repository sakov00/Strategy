using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._GlobalData;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Registries;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameObjects.EnemyRoads
{
    [Serializable]
    [RequireComponent(typeof(SplineContainer))]
    public class EnemyRoad : MonoBehaviour, IClearData
    {
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private EnemyRoadRegistry _enemyRoadRegistry;
        [Inject] private ClearDataRegistry _clearDataRegistry;

        [SerializeField] private List<EnemyGroup> _roundEnemyList = new();
        [SerializeField] private SplineContainer _splineContainer = new();
        [SerializeField] private List<SplineAnimate> _splineAnimates;
        [SerializeField] private List<TextMeshPro> _enemyIcons;
        [SerializeField] private float _distanceBetweenIcons = 4;

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
            _clearDataRegistry.Register(this);
            _enemyRoadRegistry.Register(this);
            
            foreach (var knot in _splineContainer.Spline)
            {
                Vector3 localPosition = knot.Position;
                Vector3 worldPosition = _splineContainer.transform.TransformPoint(localPosition);
                _worldPositions.Add(worldPosition);
            }
            for (int i = 0; i < _splineAnimates.Count; i++)
            {
                var splineAnimate = _splineAnimates[i];
                splineAnimate.StartOffset = i / (float)_splineAnimates.Count;
            }
            
            var wayLenght = _splineContainer.Spline.GetLength();
            for (int i = 0; i < _enemyIcons.Count; i++)
            {
                var percentIcon = (wayLenght - (i + 1) * _distanceBetweenIcons) / wayLenght;
                _enemyIcons[i].transform.position = _splineContainer.EvaluatePosition(percentIcon);
                _enemyIcons[i].transform.position += new Vector3(0, 0.1f, 0);
            }
            RefreshView();
        }

        public void StartSpawn()
        {
            int currentRound = AppData.LevelData.CurrentRound;
            if (currentRound >= _roundEnemyList.Count)
            {
                Debug.LogWarning("Нет настроек для текущего раунда спавна.");
                return;
            }

            _currentEnemyList = _roundEnemyList[currentRound].enemies;
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

        public void RefreshView()
        {
            for (int i = 0; i < Enum.GetValues(typeof(UnitType)).Length; i++)
            {
                var countEnemy = _roundEnemyList[AppData.LevelData.CurrentRound].enemies.Count(x => (int)x.enemyType == i);
                if (countEnemy > 0)
                {
                    _enemyIcons[i].gameObject.SetActive(true);
                    _enemyIcons[i].text = countEnemy.ToString();
                }
                else
                    _enemyIcons[i].gameObject.SetActive(false); 
            }
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
