using System;
using System.Collections.Generic;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
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
    public class EnemyRoadController : MonoBehaviour, ISavableController, IPoolableDispose
    {
        [Inject] private AppData _appData;
        [Inject] private UnitPool _unitPool;
        [Inject] private GameTimer _gameTimer;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] private SplineContainer _splineContainer;
        
        private List<EnemyWithTime> _currentEnemyList;
        
        [field: SerializeField] public EnemyRoadModel Model { get; private set; }
        [field: SerializeField] public EnemyRoadView View { get; private set; }
        
        public int CountRounds => Model.RoundEnemyList.Count;

        private void Awake() => Initialize();
        public void Initialize()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);

            Model.SplineContainerData = _splineContainer.ToData();

            Model.WorldPositions.Clear();
            foreach (var knot in _splineContainer.Spline)
            {
                var worldPosition = _splineContainer.transform.TransformPoint(knot.Position);
                Model.WorldPositions.Add(worldPosition);
            }

            View.Initialize(_splineContainer);
            View.RefreshInfoRound(_splineContainer, Model.RoundEnemyList);
        }

        private void OnValidate()
        {
            _splineContainer ??= GetComponent<SplineContainer>();
        }

        public ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is EnemyRoadModel buildingZoneModel)
            {
                Model = buildingZoneModel;
                _splineContainer.ApplyData(Model.SplineContainerData);
            }
        }

        public void StartSpawn()
        {
            var currentRound = _appData.LevelData.CurrentRound;
            if (currentRound >= Model.RoundEnemyList.Count)
            {
                Debug.LogWarning("Нет настроек для текущего раунда спавна.");
                return;
            }

            _currentEnemyList = Model.RoundEnemyList[currentRound].enemies;
            _currentEnemyList.Sort((a, b) => a.time.CompareTo(b.time));
            Model.CurrentIndex = 0;
            Model.ElapsedTime = 0f;

            _gameTimer.OnEverySecond -= Tick;
            _gameTimer.OnEverySecond += Tick;
        }

        private void Tick()
        {
            Model.ElapsedTime += 1f;

            while (Model.CurrentIndex < _currentEnemyList.Count &&
                   Model.ElapsedTime >= _currentEnemyList[Model.CurrentIndex].time)
            {
                Spawn(_currentEnemyList[Model.CurrentIndex]);
                Model.CurrentIndex++;
            }

            if (Model.CurrentIndex >= _currentEnemyList.Count) _gameTimer.OnEverySecond -= Tick;
        }

        private void Spawn(EnemyWithTime enemyData)
        {
            var offsetX = Random.Range(-5f, 5f);
            var wayPoints = new List<Vector3>();
            foreach (var position in Model.WorldPositions) wayPoints.Add(position + new Vector3(offsetX, 0f, 0f));
            var enemyController = _unitPool.Get(enemyData.enemyType, wayPoints[0]);
            enemyController.Initialize();
            enemyController.SetWayToPoint(wayPoints);
        }
        
        public void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            _gameTimer.OnEverySecond -= Tick;
            _objectsRegistry.Unregister(this);
        }
    }
}