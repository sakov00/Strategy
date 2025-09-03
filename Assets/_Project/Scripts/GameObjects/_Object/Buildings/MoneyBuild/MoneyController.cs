using System.Linq;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using UniRx;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.MoneyBuild
{
    public class MoneyController
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private readonly MoneyBuildModel _moneyBuildModel;
        private readonly MoneyBuildingView _moneyBuildingView;
        
        private readonly CompositeDisposable _disposables = new();

        public MoneyController(MoneyBuildModel moneyBuildModel, MoneyBuildingView moneyBuildingView)
        {
            _moneyBuildModel = moneyBuildModel;
            _moneyBuildingView = moneyBuildingView;
            
            InjectManager.Inject(this);
            _objectsRegistry
                .GetTypedList<UnitController>()
                .ObserveRemove()
                .Skip(1)
                .Subscribe(_ => AllEnemiesDestroyed())
                .AddTo(_disposables);
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_objectsRegistry.GetTypedList<UnitController>().Any(x => x.Model.WarSide == WarSide.Enemy)) 
                return;

            AddMoneyToPlayer();
        }

        private void AddMoneyToPlayer()
        {
            var moneyAmount = _moneyBuildModel.AddMoneyValue * (_moneyBuildModel.CurrentLevel + 1);
            AppData.User.Money += moneyAmount;
            _moneyBuildingView.MoneyUp(moneyAmount);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}