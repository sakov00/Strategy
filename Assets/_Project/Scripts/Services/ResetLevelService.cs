using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.Environment;
using _Project.Scripts.GameObjects.Units.Unit;
using _Project.Scripts.Interfaces.Model;
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
            if (_objectsRegistry.GetTypedList<UnitController>().Any(x => x.ObjectModel.WarSide == WarSide.Enemy)) 
                return;

            AppData.LevelData.CurrentRound++;
            NewRound();
        }
        
        private void NewRound()
        {
            foreach (var healthModel in _objectsRegistry.GetAllByInterface<IHealthModel>())
            {
                if (healthModel.WarSide == WarSide.Friend)
                {
                    healthModel.CurrentHealth = healthModel.MaxHealth;
                    healthModel.Transform.position = healthModel.NoAimPos;
                    healthModel.Transform.gameObject.SetActive(true);
                }
            }
            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoad>())
                spawn.RefreshInfoRound();
        }
        
        public void ResetLevel()
        {
            foreach (var obj in _objectsRegistry.GetTypedList<ObjectController>())
                obj.ClearData();
            foreach (var obj in _objectsRegistry.GetTypedList<EnvironmentController>())
                obj.ClearData();
            foreach (var obj in _objectsRegistry.GetTypedList<TerrainController>())
                obj.ClearData();
            foreach (var obj in _objectsRegistry.GetTypedList<EnemyRoad>())
                obj.ClearData();
            foreach (var obj in _objectsRegistry.GetTypedList<BuildingZoneController>())
                obj.ClearData();
            
            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoad>())
                spawn.RefreshInfoRound();
            
            _objectsRegistry.Clear();
            _windowsManager.GetWindow<GameWindowView>().Reset();
        }
    }
}