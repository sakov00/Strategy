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
            foreach (var friendUnit in view.buildUnitPositions)
            {
                model.buildUnits.Add(friendFactory.CreateFriendUnit(model.unitType, 
                    friendUnit.position, friendUnit.position));
            }
        }
    }
}