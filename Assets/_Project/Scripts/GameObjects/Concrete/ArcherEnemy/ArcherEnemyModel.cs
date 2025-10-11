using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;

namespace _Project.Scripts.GameObjects.Concrete.ArcherEnemy
{
    [Serializable]
    [MemoryPackable]
    public partial class ArcherEnemyModel : UnitModel
    {
        public override ISavableModel GetSaveData()
        {
            var model = new ArcherEnemyModel();
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}