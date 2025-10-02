using System;
using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    public class LevelData : IDisposable
    {
        private readonly IntReactiveProperty _levelMoney = new(30);
        private readonly BoolReactiveProperty _isFighting = new(false);
        public IReactiveProperty<int> LevelMoneyReactive => _levelMoney;
        public IReactiveProperty<bool> IsFightingReactive => _isFighting;
        
        public int LevelMoney
        {
            get => _levelMoney.Value;
            set => _levelMoney.Value = value;
        }
        
        public bool IsFighting
        {
            get => _isFighting.Value;
            set => _isFighting.Value = value;
        }
        
        public Vector3 MoveDirection { get; set; }


        public void Dispose()
        {
            _levelMoney?.Dispose();
        }
    }
}