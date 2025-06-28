using System;
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
        [Inject] private FriendFactory friendFactory;
        
        [SerializeField] private FriendsBuildModel model;
        [SerializeField] private FriendsBuildView view;
        public override BuildModel BuildModel => model;
        public override BuildView BuildView => view;
        
        private FriendsCreatorController friendsCreatorController;
        
        protected override void Initialize()
        {
#if EDIT_MODE
            _saveRegistry.Register(this);
#endif
            friendsCreatorController = new FriendsCreatorController(friendFactory, model, view);
            base.Initialize();
        }
        
        public FriendsBuildJson GetJsonData()
        {
            var friendsBuildJson = new FriendsBuildJson();
            friendsBuildJson.position = transform.position;
            friendsBuildJson.rotation = transform.rotation;
            friendsBuildJson.friendsBuildModel = model;
            return friendsBuildJson;
        }

        public void SetJsonData(FriendsBuildJson friendsBuildJson)
        {
            transform.position = friendsBuildJson.position;
            transform.rotation = friendsBuildJson.rotation;
            model = friendsBuildJson.friendsBuildModel;
        }

        private void Start()
        {
            friendsCreatorController.CreateFriends();
        }
    }
}