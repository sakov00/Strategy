using _General.Scripts.Registries;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
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
            foreach (var obj in _objectsRegistry.GetAllByInterface<ObjectController>())
                obj.Dispose();
        }
        
        public void ResetLevel()
        {
            foreach (var obj in _objectsRegistry.GetAllByInterface<IPoolableDispose>())
                obj.Dispose();
            
            _idsRegistry.Clear();
            _objectsRegistry.Clear();
        }
    }
}