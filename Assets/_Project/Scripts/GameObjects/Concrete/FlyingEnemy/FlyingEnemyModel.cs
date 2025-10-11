using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;

namespace _Project.Scripts.GameObjects.Concrete.FlyingEnemy
{
    [Serializable]
    [MemoryPackable]
    public partial class FlyingEnemyModel : UnitModel
    {
        public override ISavableModel GetSaveData()
        {
            var model = new FlyingEnemyModel();
            FillObjectModelData(model);
            FillUnitModelData(model);
            return model;
        }
    }
}