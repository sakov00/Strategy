using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.Interfaces;
using VContainer;

namespace _General.Scripts.Services
{
    public class ResetLevelService
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private IdsRegistry _idsRegistry;
        
        public void ResetRound()
        {
            foreach (var o in _objectsRegistry.GetAllByType<ObjectController>())
            {
                if (o is not PlayerController)
                    o.Dispose();
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