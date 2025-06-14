using System;
using UniRx;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class LevelData
    {
        private IntReactiveProperty currentRound = new (0);
        private IntReactiveProperty money = new(0);
        public IReactiveProperty<int> CurrentRoundReactive => currentRound;
        public IReactiveProperty<int> MoneyReactive => money;

        public int CurrentRound
        {
            get => currentRound.Value;
            set => currentRound.Value = value;
        }
        
        public int Money
        {
            get => money.Value;
            set => money.Value = value;
        }
    }
}