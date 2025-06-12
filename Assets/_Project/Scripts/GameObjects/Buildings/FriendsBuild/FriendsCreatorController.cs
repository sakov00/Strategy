using _Project.Scripts.Factories;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsCreatorController
    {
        private readonly FriendFactory friendFactory;
        private readonly FriendsBuildModel model;
        private readonly FriendsBuildView view;

        public FriendsCreatorController(FriendFactory friendFactory, FriendsBuildModel model, FriendsBuildView view)
        {
            this.friendFactory = friendFactory;
            this.model = model;
            this.view = view;
        }

        public void CreateFriends()
        {
            for (int i = 0; i < model.countUnits; i++)
            {
                model.buildUnits.Add(friendFactory.CreateFriendUnit(model.unitType, view.buildUnitPositions[i].position));
            }
        }
    }
}