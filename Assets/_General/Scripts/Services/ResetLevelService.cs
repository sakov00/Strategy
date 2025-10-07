using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using VContainer;

namespace _General.Scripts.Services
{
    public class ResetLevelService
    {
        [Inject] private UnitPool _unitPool;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private IdsRegistry _idsRegistry;
        
        public void ResetRound()
        {
            foreach (var o in _objectsRegistry.GetAllByType<ObjectController>())
            {
                if (o is PlayerController)
                {
                    o.Initialize();
                }
                else
                    o.Dispose();
            }

            var player = _unitPool.GetAvailableUnits().FirstOrDefault(x => x.UnitType == UnitType.Player);
            if (player != null)
            {
                player.Initialize();
                _unitPool.Remove(player);
            }
        }
        
        public void ResetLevel()
        {
            foreach (var obj in _objectsRegistry.GetAllByType<IDestroyable>())
                obj.Destroy();
            
            foreach (var obj in _objectsRegistry.GetAllByType<IPoolableDispose>())
                obj.Dispose();
            
            _idsRegistry.Clear();
            _objectsRegistry.Clear();
        }
    }
}