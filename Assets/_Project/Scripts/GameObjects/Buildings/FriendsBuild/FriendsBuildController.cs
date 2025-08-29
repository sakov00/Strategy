using System.Linq;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Units.Unit;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsBuildController : BuildController, IJsonSerializable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private CharacterPool _characterPool;
        
        [SerializeField] private FriendsBuildModel _model;
        [SerializeField] private FriendsBuildView _view;
        public override BuildModel BuildModel => _model;
        public override BuildView BuildView => _view;
        
        
        protected override void Initialize()
        {
            base.Initialize();
            _objectsRegistry.Register(this);
        }

        private void Start()
        {
            if(_model.BuildUnits.Count == 0)
                CreateFriends();
        }
        
        public void CreateFriends()
        {
            foreach (var friendUnit in _view._buildUnitPositions)
            {
                var unitController = _characterPool.Get<UnitController>(_model.CharacterType, friendUnit.position);
                _model.BuildUnits.Add(unitController);
            }
        }
        
        public override ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(FriendsBuildController),
                position = transform.position,
                rotation = transform.rotation
            };
            return objectJson;
        }

        public override void SetJsonData(ObjectJson objectJson)
        {
            transform.position = objectJson.position;
            transform.rotation = objectJson.rotation;
        }

        public override void ClearData()
        {
            Destroy(gameObject);
        }
    }
}