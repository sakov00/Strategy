using _General.Scripts.Json;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.GameObjects.Units.Unit;
using _Project.Scripts.Interfaces;

namespace _Project.Scripts.GameObjects.Units.Friends.ArcherFriend
{
    public class ArcherFriendController : UnitController
    {
        public override ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(ArcherFriendController),
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
    }
}