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
            AddFriends();
        }
        
        private void AddFriends()
        {
            if (Model.BuildUnitIds.Count == 0)
            {
                foreach (var friendUnit in View._buildUnitPositions)
                {
                    var unitController = _unitPool.Get(Model.UnitType, friendUnit.position);
                    unitController.SetWayToPoint(new List<Vector3> { friendUnit.position });
                    Model.BuildUnitIds.Add(unitController.Id);
                    unitController.OnKilled += CheckRemovedUnits;
                }  
            }
            else
            {
                foreach (var unitId in Model.BuildUnitIds)
                {
                   var unit = (UnitController)IdsRegistry.Get(unitId);
                   unit.OnKilled += CheckRemovedUnits;
                }
            }
        }

        private void CheckRemovedUnits(UnitController unitController)
        {
            Model.BuildUnitIds.Remove(unitController.Id);
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
                var unitController = _unitPool.Get(Model.UnitType, transform.position);
                Model.BuildUnitIds.Add(unitController.Id);
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
        
        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            _gameTimer.OnEverySecond -= HandleSpawnTimer;
            if(Model.BuildUnitIds.Count == 0) return;
            foreach (var unitId in Model.BuildUnitIds)
            {
                var unit = (UnitController)IdsRegistry.Get(unitId);
                unit.OnKilled -= CheckRemovedUnits;
            }
        }
    }
}