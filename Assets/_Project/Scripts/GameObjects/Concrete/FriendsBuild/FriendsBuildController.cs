using System.Collections.Generic;
using _General.Scripts._GlobalLogic;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Concrete.FriendsBuild
{
    public class FriendsBuildController : BuildController
    {
        [Inject] private UnitPool _unitPool;
        [Inject] private GameTimer _gameTimer;
        
        [field: SerializeField] public FriendsBuildModel Model { get; set; }
        [field: SerializeField] public FriendsBuildView View { get; set; }
        protected override BuildModel BuildModel => Model;
        protected override BuildView BuildView => View;

        public override void Initialize()
        {
            base.Initialize();
            Model.CurrentHealth = Model.MaxHealth;

            View.Initialize();
            CreateFriends();
        }
        
        private void CreateFriends()
        {
            foreach (var friendUnit in View._buildUnitPositions)
            {
                var unitController = _unitPool.Get(Model.UnitType, friendUnit.position);
                unitController.SetWayToPoint(new List<Vector3> { friendUnit.position });
                Model.BuildUnits.Add(unitController);
                unitController.OnKilled += CheckRemovedUnits;
            }
        }

        private void CheckRemovedUnits(UnitController unitController)
        {
            Model.NeedRestoreUnitsCount++;

            if (Model.NeedRestoreUnitsCount > 0 && Model.CurrentSpawnTimer < 0)
            {
                Model.CurrentSpawnTimer = 0;
                _gameTimer.OnEverySecond += HandleSpawnTimer;
            }
        }

        private void HandleSpawnTimer()
        {
            Model.CurrentSpawnTimer++;
            View.UpdateLoadBar(Model.CurrentSpawnTimer, Model.TimeCreateUnits);

            if (Model.CurrentSpawnTimer < Model.TimeCreateUnits) return;

            if (Model.NeedRestoreUnitsCount > 0)
            {
                _unitPool.Get(Model.UnitType, transform.position);
                Model.NeedRestoreUnitsCount--;
            }

            if (Model.NeedRestoreUnitsCount > 0)
            {
                Model.CurrentSpawnTimer = 0;
            }
            else
            {
                _gameTimer.OnEverySecond -= HandleSpawnTimer;
                Model.CurrentSpawnTimer = -1;
            }
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is FriendsBuildModel friendsBuildModel)
            {
                Model = friendsBuildModel;
            }
        }
        
        public override void Dispose(bool returnToPool = true)
        {
            base.Dispose(returnToPool);
            _gameTimer.OnEverySecond -= HandleSpawnTimer;
            Model.BuildUnits.ForEach(unitController => unitController.OnKilled -= CheckRemovedUnits);
        }
    }
}