using System;
using System.Collections.Generic;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.Extentions;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameObjects.Additional.EnemyRoads
{
    [Serializable]
    [RequireComponent(typeof(SplineContainer))]
    public class EnemyRoadController : MonoBehaviour, ISavableController, IDestroyable
    {
        [Inject] private AppData _appData;
        [Inject] private UnitPool _unitPool;
        [Inject] private GameTimer _gameTimer;
        [Inject] private SaveRegistry _saveRegistry;
        
        [SerializeField] private EnemyRoadModel _model;
        [SerializeField] private EnemyRoadView _view;
        [SerializeField] private SplineContainer _splineContainer;

        private List<EnemyWithTime> _currentEnemyList;
        public int CountRounds => _model.RoundEnemyList.Count;

        private void OnValidate()
        {
            _splineContainer ??= GetComponent<SplineContainer>();
        }

        private void Awake()
        {
            InjectManager.Inject(this);
        }

        private void Start()
        {
            if (_appData.AppMode == AppMode.Redactor)
                Initialize();
        }

        public void Initialize()
        {
            _saveRegistry.Register(this);

            _model.SplineContainerData = _splineContainer.ToData();

            _model.WorldPositions.Clear();
            foreach (var knot in _splineContainer.Spline)
            {
                var worldPosition = _splineContainer.transform.TransformPoint(knot.Position);
                _model.WorldPositions.Add(worldPosition);
            }
            
            StartSpawn(false);

            _view.Initialize(_splineContainer);
            _view.RefreshInfoRound(_splineContainer, _model.RoundEnemyList);
        }

        public ISavableModel GetSavableModel()
        {
            _model.SavePosition = transform.position;
            _model.SaveRotation = transform.rotation;
            return _model;
        }

        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is EnemyRoadModel buildingZoneModel)
            {
                _model = buildingZoneModel;
                _splineContainer.ApplyData(_model.SplineContainerData);
            }
        }

        public void StartSpawn(bool isNew = true)
        {
            if (_appData.LevelData.IsFighting == false && isNew == false) return;
            
            var currentRound = _appData.LevelData.CurrentRound;
            if (currentRound >= _model.RoundEnemyList.Count)
            {
                Debug.LogWarning("Нет настроек для текущего раунда спавна.");
                return;
            }

            _currentEnemyList = _model.RoundEnemyList[currentRound].enemies;
            _currentEnemyList.Sort((a, b) => a.time.CompareTo(b.time));
            if (isNew)
            {
                _model.CurrentIndex = 0;
                _model.ElapsedTime = 0f;
            }

            _gameTimer.OnEverySecond -= Tick;
            _gameTimer.OnEverySecond += Tick;
        }

        private void Tick()
        {
            _model.ElapsedTime += 1f;

            while (_model.CurrentIndex < _currentEnemyList.Count &&
                   _model.ElapsedTime >= _currentEnemyList[_model.CurrentIndex].time)
            {
                Spawn(_currentEnemyList[_model.CurrentIndex]);
                _model.CurrentIndex++;
            }

            if (_model.CurrentIndex >= _currentEnemyList.Count) _gameTimer.OnEverySecond -= Tick;
        }

        private void Spawn(EnemyWithTime enemyData)
        {
            var offsetX = Random.Range(-5f, 5f);
            var wayPoints = new List<Vector3>();
            foreach (var position in _model.WorldPositions) wayPoints.Add(position + new Vector3(offsetX, 0f, 0f));
            var enemyController = _unitPool.Get(enemyData.enemyType, wayPoints[0]);
            enemyController.Initialize();
            enemyController.SetWayToPoint(wayPoints);
        }
        
        public void Destroy() => Destroy(gameObject);
        
        private void OnDestroy()
        {
            _gameTimer.OnEverySecond -= Tick;
            _saveRegistry.Unregister(this);
        }
    }
}