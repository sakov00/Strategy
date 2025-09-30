using _General.Scripts._GlobalLogic;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Concrete.FriendsBuild
{
    public class FriendsBuildController : BuildController
    {
        [Inject] private CharacterPool _characterPool;
        [Inject] private GameTimer _gameTimer;
        
        [field: SerializeField] public FriendsBuildModel Model { get; set; }
        [field: SerializeField] public FriendsBuildView View { get; set; }
        
        public override WarSide WarSide => Model.WarSide;
        public override float CurrentHealth { get => Model.CurrentHealth; set => Model.CurrentHealth = value; }

        public override void Initialize()
        {
            InjectManager.Inject(this);

            View.Initialize();
            HeightObject = View.GetHeightObject();
            Model.NoAimPos = transform.position;
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
                var unitController = _characterPool.Get<UnitController>(Model.UnitType, friendUnit.position);
                Model.SaveBuildUnits.Add(unitController.Model);
                Model.BuildUnits.Add(unitController);
                unitController.OnKilled += CheckRemovedUnits;
            }
        }

        private void CreateFriendsOnLoad()
        {
            foreach (var saveBuildUnit in Model.SaveBuildUnits)
            {
                var unitController = _characterPool.Get<UnitController>(Model.UnitType);
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
            transform.SetParent(null);
            Model.CurrentHealth = Model.MaxHealth;
            Model.NoAimPos = transform.position;
            BuildPool.Remove(this);
            gameObject.SetActive(true);
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