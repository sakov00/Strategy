using System;
using UniRx;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class LevelData
    {
        private IntReactiveProperty money = new(0);
        public IReactiveProperty<int> MoneyReactive => money;

        public int Money
        {
            get => money.Value;
            set => money.Value = value;
        }
    }
}