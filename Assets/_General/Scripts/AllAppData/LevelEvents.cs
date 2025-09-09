using System;
using System.Linq;
using _General.Scripts._VContainer;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _General.Scripts.AllAppData
{
    public class LevelEvents : IInitializable, IDisposable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;

        public event Action AllEnemiesKilled;
        public event Action MainBuildDestroyed;

        private CompositeDisposable _disposables;

        public void Initialize()
        {
            InjectManager.Inject(this);
            _disposables = new CompositeDisposable();
            _objectsRegistry
                .GetTypedList<UnitController>()
                .ObserveRemove()
                .Subscribe(_ => TryInvokeAllEnemiesKilled())
                .AddTo(_disposables);
        }

        private void TryInvokeAllEnemiesKilled()
        {
            if (_objectsRegistry.GetTypedList<UnitController>().Any(x => x.Model.WarSide == WarSide.Enemy))
                return;

            AllEnemiesKilled?.Invoke();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}