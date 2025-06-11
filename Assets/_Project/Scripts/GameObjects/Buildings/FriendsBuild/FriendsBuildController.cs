using System;
using _Project.Scripts.GameObjects._General;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [RequireComponent(typeof(HealthBarView))]
    public class FriendsBuildController : BuildController
    {
        [SerializeField] private FriendsBuildModel model;
        //[SerializeField] private MoneyBuildingView view;
        [SerializeField] private HealthBarView healthBarView;
        
        private FriendsCreatorController friendsCreatorController;
        
        private void Awake()
        {
            BuildModel = model;
            //BuildView = view;
            
            friendsCreatorController = new FriendsCreatorController(model);
        }

        private void Start()
        {
            friendsCreatorController.CreateFriends();
        }

        private void Update()
        {
            healthBarView.UpdateView();
        }

        private void FixedUpdate()
        {
            healthBarView.UpdateView();
        }
    }
}