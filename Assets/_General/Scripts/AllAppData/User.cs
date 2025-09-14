using UniRx;

namespace _General.Scripts.AllAppData
{
    public class User
    {
        private readonly IntReactiveProperty _globalMoney = new(30);
        private readonly IntReactiveProperty _currentLevel = new (0);
        private readonly IntReactiveProperty _currentRound = new (0);
        
        public IReactiveProperty<int> GlobalMoneyReactive => _globalMoney;
        public IReactiveProperty<int> CurrentLevelReactive => _currentLevel;
        public IReactiveProperty<int> CurrentRoundReactive => _currentRound;
        
        public int GlobalMoney
        {
            get => _globalMoney.Value;
            set => _globalMoney.Value = value;
        }
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
        
        public void Dispose()
        {
            _currentLevel?.Dispose();
            _currentRound?.Dispose();
            _globalMoney?.Dispose();
        }
    }
}