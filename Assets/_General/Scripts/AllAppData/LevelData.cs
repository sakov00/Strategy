using System;
using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    public class LevelData : IDisposable
    {
        private readonly IntReactiveProperty _levelMoney = new(30);
        public IReactiveProperty<int> LevelMoneyReactive => _levelMoney;
        
        public int Money
        {
            get => _levelMoney.Value;
            set => _levelMoney.Value = value;
        }
        
        public Vector3 MoveDirection { get; set; }


        public void Dispose()
        {
            _levelMoney?.Dispose();
        }
    }
}