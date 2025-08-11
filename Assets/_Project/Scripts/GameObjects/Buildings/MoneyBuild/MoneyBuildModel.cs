using System;
using UnityEngine;

namespace _Project.Scripts.GameObjects.MoneyBuild
{
    [Serializable]
    public class MoneyBuildModel : BuildModel
    {
        [Header("Money")]
        public int _addMoneyValue = 1;

        public int AddMoneyValue
        {
            get => _addMoneyValue;
            set => _addMoneyValue = value;
        }
    }
}