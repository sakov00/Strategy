using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.Characters.Unit;

namespace _Project.Scripts.GameObjects._Object.Characters.Friends.WarriorFriend
{
    public class WarriorFriendController : UnitController
    {
        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is UnitModel unitModel)
            {
                Model = unitModel;
                Initialize();
            }
        }
    }
}