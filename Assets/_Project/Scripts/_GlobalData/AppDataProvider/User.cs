using UniRx;

namespace _Project.Scripts._GlobalData.AppDataProvider
{
    public class User
    {
        private readonly IntReactiveProperty _money = new(30);

        public IReactiveProperty<int> MoneyReactive => _money;
        
        public int Money
        {
            get => _money.Value;
            set => _money.Value = value;
        }
    }
}