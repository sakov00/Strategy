using System;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Concrete.Player;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class MoneyBuildModel : BuildModel
    {
        [Header("Money")] 
        [MemoryPackInclude][field:SerializeField] public int AddMoneyValue { get; set; } = 1;
        
        public override void LoadFrom(ISavableModel model)
        {
            base.LoadFrom(model);
            if (model is MoneyBuildModel objectModel)
            {
                AddMoneyValue = objectModel.AddMoneyValue;
            }
        }
    }
}