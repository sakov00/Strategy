using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;

namespace _Project.Scripts.GameObjects.Concrete.WarriorFriend
{
    [Serializable]
    [MemoryPackable]
    public partial class WarriorFriendModel : UnitModel
    {
        public override ISavableModel GetSaveData()
        {
            var model = new WarriorFriendModel();
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}