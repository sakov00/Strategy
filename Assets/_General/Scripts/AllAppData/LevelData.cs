using System;
using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    public class LevelData : IDisposable
    {
        private readonly IntReactiveProperty _currentLevel = new (0);
        private readonly IntReactiveProperty _currentRound = new (0);
        private readonly IntReactiveProperty _money = new(30);
        
        public IReactiveProperty<int> CurrentLevelReactive => _currentLevel;
        public IReactiveProperty<int> CurrentRoundReactive => _currentRound;
        
        public IReactiveProperty<int> MoneyReactive => _money;
        
        public int CurrentLevel
        {
            get => _currentLevel.Value;
            set => _currentLevel.Value = value;
        }
        
        public int CurrentRound
        {
            get => _currentRound.Value;
            set => _currentRound.Value = value;
        }
        
        public int Money
        {
            get => _money.Value;
            set => _money.Value = value;
        }
        
        public Vector3 MoveDirection { get; set; }


        public void Dispose()
        {
            _currentLevel.Dispose();
            _currentRound.Dispose();
            _money.Dispose();
        }
    }
}