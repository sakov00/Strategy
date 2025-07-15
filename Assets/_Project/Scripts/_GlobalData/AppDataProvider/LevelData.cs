using UniRx;
using UnityEngine;

namespace _Project.Scripts._GlobalData.AppDataProvider
{
    public class LevelData
    {
        private readonly IntReactiveProperty _currentLevel = new (0);
        private readonly IntReactiveProperty _currentRound = new (0);
        private readonly BoolReactiveProperty _isNextRoundAvailable = new (true);
                        
        public IReactiveProperty<int> CurrentLevelReactive => _currentLevel;
        public IReactiveProperty<int> CurrentRoundReactive => _currentRound;
        
        public IReactiveProperty<bool> IsNextRoundAvailableReactive => _isNextRoundAvailable;
        
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
        
        public bool IsNextRoundAvailable
        {
            get => _isNextRoundAvailable.Value;
            set => _isNextRoundAvailable.Value = value;
        }
        public Vector3 MoveDirection { get; set; }
    }
}