using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    public class LevelData
    {
        private readonly IntReactiveProperty _currentLevel = new (0);
        private readonly IntReactiveProperty _currentRound = new (0);
        
        public IReactiveProperty<int> CurrentLevelReactive => _currentLevel;
        public IReactiveProperty<int> CurrentRoundReactive => _currentRound;
        
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
        
        public Vector3 MoveDirection { get; set; }
    }
}