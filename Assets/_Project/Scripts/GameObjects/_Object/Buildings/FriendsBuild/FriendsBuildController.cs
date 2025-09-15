using System.Linq;
using _General.Scripts._GlobalLogic;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.Pools;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.FriendsBuild
{
    public class FriendsBuildController : BuildController
    {
        [Inject] private GameTimer _gameTimer;
        [Inject] private CharacterPool _characterPool;
        
        [field:SerializeField] public FriendsBuildModel Model { get; set; }
        [field:SerializeField] public FriendsBuildView View { get; set; }
        public override BuildModel BuildModel => Model;
        public override BuildView BuildView => View;
        
        public override void Initialize()
        {
            base.Initialize();
            ObjectsRegistry.Register(this);
        }

        public override void BuildInGame()
        {
            Model.SaveBuildUnits.Clear();
            Model.BuildUnits.Clear();
            CreateFriendsOnGame();
        }
        
        private void CreateFriendsOnGame()
        {
            foreach (var friendUnit in View._buildUnitPositions)
            {
                var unitController = _characterPool.Get<UnitController>(Model.CharacterType, friendUnit.position);
                Model.SaveBuildUnits.Add(unitController.Model);
                Model.BuildUnits.Add(unitController);
                unitController.OnKilled += CheckRemovedUnits;
            }
        }
        
        private void CreateFriendsOnLoad()
        {
            foreach (var saveBuildUnit in Model.SaveBuildUnits)
            {
                var unitController = _characterPool.Get<UnitController>(Model.CharacterType);
                Model.BuildUnits.Add(unitController);
                unitController.Model = saveBuildUnit;
                unitController.transform.position += unitController.Model.SavePosition;
                unitController.OnKilled += CheckRemovedUnits;
            }
        }
        
        private void CheckRemovedUnits(UnitController unitController)
        {
            if (Model.SpawnQueue.Contains(unitController)) return;
            
            Model.SpawnQueue.Enqueue(unitController);

            if (Model.SpawnQueue.Count > 0 && Model.CurrentSpawnTimer < 0)
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

            if (Model.SpawnQueue.Count > 0)
            {
                var friendUnit = Model.SpawnQueue.Dequeue();
                _characterPool.Remove(friendUnit);
                friendUnit.Restore();
            }

            if (Model.SpawnQueue.Count > 0)
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
                Initialize();
                CreateFriendsOnLoad();
            }
        }

        public override void Restore()
        {
            base.Restore();
            ObjectsRegistry.Register(this);
            _gameTimer.OnEverySecond -= HandleSpawnTimer;
        }

        public override void ReturnToPool()
        {
            BuildPool.Return(this);
            _gameTimer.OnEverySecond -= HandleSpawnTimer;
            Model.BuildUnits.ForEach(unitController => unitController.OnKilled -= CheckRemovedUnits);
        }

        public override void ClearData()
        {
            ObjectsRegistry.Unregister(this);
            _gameTimer.OnEverySecond -= HandleSpawnTimer;
            Model.BuildUnits.ForEach(unitController => unitController.OnKilled -= CheckRemovedUnits);
        }
    }
}