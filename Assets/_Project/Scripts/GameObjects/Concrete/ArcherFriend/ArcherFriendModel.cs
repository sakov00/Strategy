using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;

namespace _Project.Scripts.GameObjects.Concrete.ArcherFriend
{
    [Serializable]
    [MemoryPackable]
    public partial class ArcherFriendModel : UnitModel
    {
        public override ISavableModel GetSaveData()
        {
            var model = new ArcherFriendModel();
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}