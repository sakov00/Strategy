using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Unit;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    public class FriendsCreatorController
    {
        private readonly FriendFactory _friendFactory;
        private readonly FriendsBuildModel _model;
        private readonly FriendsBuildView _view;

        public FriendsCreatorController(FriendFactory friendFactory, FriendsBuildModel model, FriendsBuildView view)
        {
            _friendFactory = friendFactory;
            _model = model;
            _view = view;
        }

        public void CreateFriends()
        {
            foreach (var friendUnit in _view._buildUnitPositions)
            {
                var unitController = _friendFactory.CreateFriendUnit(_model.UnitType, friendUnit.position);
                _model.BuildUnits.Add(unitController);
            }
        }
    }
}