using System.Collections.Generic;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.Interfaces;

namespace _Project.Scripts.Registries
{
    public class ClearDataRegistry
    {
        private readonly HashSet<IClearData> _clearDatas = new();

        public void Register(IClearData data)
        {
            if (!_clearDatas.Contains(data))
                _clearDatas.Add(data);
        }

        public void Unregister(IClearData data)
        {
            if (_clearDatas.Contains(data))
                _clearDatas.Remove(data);
        }

        public HashSet<IClearData> GetAll()
        {
            return _clearDatas;
        }

        public void Clear()
        {
            foreach (var data in _clearDatas)
            {
                data.ClearData();
            }
            _clearDatas.Clear();
        }
    }
}