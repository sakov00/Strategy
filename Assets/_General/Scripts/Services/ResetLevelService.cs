using System;
using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.Interfaces;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace _General.Scripts.Services
{
    public class ResetLevelService : IInitializable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private WindowsManager _windowsManager;
        
        public void Initialize()
        {
            _objectsRegistry
                .GetTypedList<UnitController>()
                .ObserveRemove()
                .Subscribe(_ => AllEnemiesDestroyed());
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_objectsRegistry.GetTypedList<UnitController>().Any(x => x.Model.WarSide == WarSide.Enemy)) 
                return;

            AppData.LevelData.CurrentRound++;
            NewRound();
        }
        
        private void NewRound()
        {
            foreach (var obj in _objectsRegistry.GetAllByInterface<ObjectController>())
            {
                if (obj.ObjectModel.WarSide == WarSide.Friend)
                {
                    obj.ObjectModel.CurrentHealth = obj.ObjectModel.MaxHealth;
                    obj.transform.position = obj.ObjectModel.NoAimPos;
                    obj.transform.gameObject.SetActive(true);
                }
            }
            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawn.RefreshInfoRound();
        }
        
        public void ResetLevel()
        {
            foreach (var obj in _objectsRegistry.GetAllByInterface<IClearData>())
            {
                obj.ClearData();
                obj.DestroyObject();
            }

            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawn.RefreshInfoRound();
            
            _objectsRegistry.Clear();
            // _windowsManager?.GetWindow<GameWindowView>().Reset();
        }
    }
}