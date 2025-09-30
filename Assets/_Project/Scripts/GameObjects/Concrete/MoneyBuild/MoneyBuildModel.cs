using System;
using _Project.Scripts.GameObjects.Abstract;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.MoneyBuild
{
    [Serializable]
    [MemoryPackable]
    public partial class MoneyBuildModel : BuildModel
    {
        [Header("Money")] public int _addMoneyValue = 1;

        public int AddMoneyValue
        {
            get => _addMoneyValue;
            set => _addMoneyValue = value;
        }
    }
}