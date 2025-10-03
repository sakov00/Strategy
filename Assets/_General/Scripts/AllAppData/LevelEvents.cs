using System;
using System.Linq;
using _General.Scripts._VContainer;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _General.Scripts.AllAppData
{
    public class LevelEvents : IInitializable, IDisposable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;

        public event Func<UniTaskVoid> WinEvent;
        public event Func<UniTaskVoid> FailEvent;

        private CompositeDisposable _disposables;

        public void Initialize()
        {
            Dispose();
            _disposables = new CompositeDisposable();
            _objectsRegistry
                .GetTypedList<UnitController>()
                .ObserveRemove()
                .Subscribe(removedUnit =>  TryInvokeAllEnemiesKilled(removedUnit.Value))
                .AddTo(_disposables);
            
            _objectsRegistry
                .GetTypedList<BuildController>()
                .ObserveRemove()
                .Subscribe(_ => TryInvokeMainBuildDestroyed())
                .AddTo(_disposables);
        }

        private void TryInvokeAllEnemiesKilled(UnitController unitController)
        {
            if (unitController.WarSide == WarSide.Friend ||
                _objectsRegistry.GetTypedList<UnitController>().Any(x => x.WarSide == WarSide.Enemy))
                return;
            
            WinEvent?.Invoke();
        }
        
        private void TryInvokeMainBuildDestroyed()
        {
            if (_objectsRegistry.GetTypedList<BuildController>().Any(x => x.BuildType == BuildType.MainBuild))
                return;

            FailEvent?.Invoke();
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}