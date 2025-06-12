using System;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._General;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [RequireComponent(typeof(HealthBarView))]
    public class FriendsBuildController : BuildController
    {
        [Inject] private FriendFactory friendFactory;
        
        [SerializeField] private FriendsBuildModel model;
        [SerializeField] private FriendsBuildView view;
        
        private FriendsCreatorController friendsCreatorController;
        
        private void Awake()
        {
            BuildModel = model;
            BuildView = view;
            
            friendsCreatorController = new FriendsCreatorController(friendFactory, model, view);
        }

        private void Start()
        {
            friendsCreatorController.CreateFriends();
        }

        private void FixedUpdate()
        {
            view.UpdateHealthBar(model.currentHealth, model.maxHealth);
        }
    }
}