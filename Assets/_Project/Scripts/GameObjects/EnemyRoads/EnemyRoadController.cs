using System;
using System.Collections.Generic;
using System.Linq;
using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.DTO;
using _General.Scripts.Extentions;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameObjects.EnemyRoads
{
    [Serializable]
    [RequireComponent(typeof(SplineContainer))]
    public class EnemyRoadController : MonoBehaviour, ISavableController, IClearData
    {
        [Inject] private AppData _appData;
        [Inject] private GameTimer _gameTimer;
        [Inject] private CharacterPool _characterPool;
        [Inject] private ObjectsRegistry _objectsRegistry;

        [SerializeField] private SplineContainer _splineContainer;
        
        [field:SerializeField] public EnemyRoadModel Model { get; private set; }
        [field:SerializeField] public EnemyRoadView View { get; private set; }

        private List<EnemyWithTime> _currentEnemyList;
        public int CountRounds => Model.RoundEnemyList.Count;
        
        private void OnValidate()
        {
            _splineContainer ??= GetComponent<SplineContainer>();
        }
        
        private void Start()
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
            int currentRound = _appData.User.CurrentRound;
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

            while (Model.CurrentIndex < _currentEnemyList.Count && Model.ElapsedTime >= _currentEnemyList[Model.CurrentIndex].time)
            {
                Spawn(_currentEnemyList[Model.CurrentIndex]);
                Model.CurrentIndex++;
            }

            if (Model.CurrentIndex >= _currentEnemyList.Count)
            {
                _gameTimer.OnEverySecond -= Tick;
            }
        }

        private void Spawn(EnemyWithTime enemyData)
        {
            var offsetX = Random.Range(-5f, 5f);
            var wayPoints = new List<Vector3>();
            foreach (var position in Model.WorldPositions)
            {
                wayPoints.Add(position + new Vector3(offsetX, 0f, 0f));
            }
            var enemyController = _characterPool.Get<UnitController>(enemyData.enemyType, wayPoints[0]);
            enemyController.Model.CurrentHealth = enemyController.Model.MaxHealth;
            enemyController.Model.WayToAim = wayPoints;
        }
        
        public void RefreshInfoRound() => View.RefreshInfoRound(_splineContainer, Model.RoundEnemyList);

        private void OnDestroy()
        {
            _gameTimer.OnEverySecond -= Tick;
            ClearData();
        }
        
        public void ClearData()
        {
            _objectsRegistry.Unregister(this);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
