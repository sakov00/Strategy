using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.Registries;
using _General.Scripts.UI.Windows;
using _General.Scripts.UI.Windows.GameWindow;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using VContainer;

namespace _General.Scripts.Services
{
    public class ResetLevelService
    {
        [Inject] private AppData _appData;
        [Inject] private BuildPool _buildPool;
        [Inject] private UnitPool _unitPool;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private IdsRegistry _idsRegistry;
        [Inject] private WindowsManager _windowsManager;
        
        public void ResetRound()
        {
            // можно сделать сохранение состояния когда игрок запускает раунд, промежуточный файл будет
        }
        
        public void ResetLevel()
        {
            foreach (var obj in _objectsRegistry.GetAllByInterface<IPoolableDispose>())
                obj.Dispose();

            foreach (var spawn in _objectsRegistry.GetTypedList<EnemyRoadController>())
                spawn.RefreshInfoRound();
            
            _idsRegistry.Clear();
            _objectsRegistry.Clear();
        }
    }
}