using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts._VContainer;
using _Project.Scripts.GameObjects;
using _Project.Scripts.Interfaces;

namespace _Project.Scripts.Registries
{
    public class SaveRegistry
    {
        private readonly Dictionary<Type, IList> _typedCollections = new();

        public void Register<T>(ISavable<T> savable)
        {
            var type = typeof(ISavable<T>);

            if (!_typedCollections.TryGetValue(type, out var list))
            {
                list = new List<ISavable<T>>();
                _typedCollections[type] = list;
            }

            var typedList = (List<ISavable<T>>)list;
            if (!typedList.Contains(savable))
            {
                typedList.Add(savable);
            }
        }

        public void Unregister<T>(ISavable<T> savable)
        {
            var type = typeof(ISavable<T>);
            if (_typedCollections.TryGetValue(type, out var list))
            {
                var typedList = (List<ISavable<T>>)list;
                if (typedList.Contains(savable))
                {
                    typedList.Remove(savable);
                }
            }
        }

        public List<ISavable<T>> GetAll<T>()
        {
            var type = typeof(ISavable<T>);

            if (_typedCollections.TryGetValue(type, out var listObj))
            {
                return (List<ISavable<T>>)listObj;
            }

            var newList = new List<ISavable<T>>();
            _typedCollections[type] = newList;
            return newList;
        }

        public void Clear()
        {
            _typedCollections.Clear();
        }
    }
}