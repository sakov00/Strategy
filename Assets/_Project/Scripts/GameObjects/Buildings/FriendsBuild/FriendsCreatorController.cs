using _Project.Scripts.Factories;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsCreatorController
    {
        [Inject] private FriendFactory friendFactory;
        private FriendsBuildModel model;

        public FriendsCreatorController(FriendsBuildModel model)
        {
            this.model = model;
        }

        public void CreateFriends()
        {
            for (int i = 0; i < model.countUnits; i++)
            {
                model.buildUnits.Add(friendFactory.CreateFriendUnit(model.unitType, model.buildUnitPositions[i].position));
            }
        }
    }
}