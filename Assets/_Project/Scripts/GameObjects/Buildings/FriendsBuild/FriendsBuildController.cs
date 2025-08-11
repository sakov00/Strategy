using System;
using System.Linq;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsBuildController : BuildController, ISavable<FriendsBuildJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private ClearDataRegistry _clearDataRegistry;
        [Inject] private FriendFactory _friendFactory;
        
        [SerializeField] private FriendsBuildModel _model;
        [SerializeField] private FriendsBuildView _view;
        public override BuildModel BuildModel => _model;
        public override BuildView BuildView => _view;
        
        private FriendsCreatorController _friendsCreatorController;
        
        protected override void Initialize()
        {
            base.Initialize();
            _saveRegistry.Register(this);
            _clearDataRegistry.Register(this);
            _friendsCreatorController = new FriendsCreatorController(_friendFactory, _model, _view);
        }
        
        public FriendsBuildJson GetJsonData()
        {
            var friendsBuildJson = new FriendsBuildJson();
            friendsBuildJson.position = transform.position;
            friendsBuildJson.rotation = transform.rotation;
            friendsBuildJson.friendsBuildModel = _model;
            friendsBuildJson.unitJsons = _model.BuildUnits.Select(x => x.GetJsonData()).ToList();
            return friendsBuildJson;
        }

        public void SetJsonData(FriendsBuildJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _model = environmentJson.friendsBuildModel;
            _friendFactory.CreateFriendUnits(environmentJson.unitJsons);
        }

        private void Start()
        {
            if(_model.BuildUnits.Count == 0)
                _friendsCreatorController.CreateFriends();
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}