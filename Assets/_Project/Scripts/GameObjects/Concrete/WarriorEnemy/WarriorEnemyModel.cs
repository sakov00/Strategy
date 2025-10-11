using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;

namespace _Project.Scripts.GameObjects.Concrete.WarriorEnemy
{
    [Serializable]
    [MemoryPackable]
    public partial class WarriorEnemyModel : UnitModel
    {
        public override ISavableModel GetSaveData()
        {
            var model = new WarriorEnemyModel();
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}