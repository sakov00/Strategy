using _General.Scripts._GlobalLogic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    public class User
    {
        private readonly IntReactiveProperty _globalMoney = new(30);
        private readonly IntReactiveProperty _currentLevel = new (0);
        
        public IReactiveProperty<int> GlobalMoneyReactive => _globalMoney;
        public IReactiveProperty<int> CurrentLevelReactive => _currentLevel;
        
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
        
        public User()
        {
            int savedLevel = PlayerPrefs.GetInt(GameConstants.PrefKeys.CurrentLevel, 0);
            _currentLevel = new IntReactiveProperty(savedLevel);

            _currentLevel
                .Skip(1)
                .Subscribe(SaveLevel);
        }
        
        private void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(GameConstants.PrefKeys.CurrentLevel, level);
            PlayerPrefs.Save();
        }
    }
}