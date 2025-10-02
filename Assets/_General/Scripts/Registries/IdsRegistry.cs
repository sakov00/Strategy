using System.Collections.Generic;
using _Project.Scripts.GameObjects.Abstract.BaseObject;

namespace _General.Scripts.Registries
{
    public class IdsRegistry
    {
        private readonly Dictionary<int, ObjectController> _dictionary = new();
        
        public void Register(ObjectController obj)
        {
            if (obj.Id != 0)
            {
                var existingId = obj.Id;
                _dictionary.TryAdd(existingId, obj);
                return;
            }
    
            var newId = GetFreeId();
            obj.Id = newId;
            _dictionary.Add(newId, obj);
        }
        
        private int GetFreeId()
        {
            var id = 1;
            while (_dictionary.ContainsKey(id))
                id++;
            return id;
        }
        
        public void Unregister(ObjectController obj)
        {
            _dictionary.Remove(obj.Id);
            obj.Id = 0;
        }

        public ObjectController Get(int id)
        {
            return _dictionary.TryGetValue(id, out var obj) ? obj : null;
        }

        public void Clear()
        {
            foreach (var obj in _dictionary.Values)
                obj.Id = 0;
            _dictionary.Clear();
        }
    }
}