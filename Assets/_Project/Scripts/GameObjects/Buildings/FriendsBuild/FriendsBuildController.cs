using System;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._General;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsBuildController : BuildController
    {
        [Inject] private FriendFactory friendFactory;
        
        [SerializeField] private FriendsBuildModel model;
        [SerializeField] private FriendsBuildView view;
        
        private FriendsCreatorController friendsCreatorController;
        
        protected override void Initialize()
        {
            BuildModel = model;
            BuildView = view;
            
            friendsCreatorController = new FriendsCreatorController(friendFactory, model, view);

            base.Initialize();
        }

        private void Start()
        {
            friendsCreatorController.CreateFriends();
        }
    }
}