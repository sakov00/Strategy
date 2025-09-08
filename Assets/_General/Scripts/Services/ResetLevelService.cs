using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using VContainer;

namespace _General.Scripts.Services
{
    public class ResetLevelService
    {
        [Inject] private AppData _appData;
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private WindowsManager _windowsManager;
        
        public void ResetRound()
        {
            foreach (var obj in _objectsRegistry.GetAllByInterface<ObjectController>())
            {
                if (obj.ObjectModel.WarSide == WarSide.Friend)
                    obj.Restore();
            }
            foreach (var obj in _buildPool.GetAvailableBuilds().Where(obj => obj.ObjectModel.WarSide == WarSide.Friend))
            {
                obj.Restore();
            }
            foreach (var obj in _characterPool.GetAvailableBuilds().Where(obj => obj.ObjectModel.WarSide == WarSide.Friend))
            {
                obj.Restore();
            }
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
            _windowsManager?.GetWindow<GameWindowPresenter>()?.Reset();
        }
    }
}