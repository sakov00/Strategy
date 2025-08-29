using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _General.Scripts.Registries
{
    public class ObjectsRegistry
    {
        private readonly Dictionary<Type, IList> _typedCollections = new();

        public void Register<T>(T draggable) where T : Component
        {
            var type = typeof(T);

            if (!_typedCollections.TryGetValue(type, out var list))
            {
                list = new ReactiveCollection<T>();
                _typedCollections[type] = list;
            }

            var typedList = (ReactiveCollection<T>)list;
            if (!typedList.Contains(draggable))
            {
                typedList.Add(draggable);
            }
        }

        public void Unregister<T>(T draggable) where T : Component
        {
            var type = typeof(T);
            if (_typedCollections.TryGetValue(type, out var list))
            {
                var typedList = (ReactiveCollection<T>)list;
                if (typedList.Contains(draggable))
                {
                    typedList.Remove(draggable);
                }
            }
        }

        public ReactiveCollection<T> GetAll<T>()
        {
            var type = typeof(T);

            if (_typedCollections.TryGetValue(type, out var listObj))
            {
                return (ReactiveCollection<T>)listObj;
            }

            var newList = new ReactiveCollection<T>();
            _typedCollections[type] = newList;
            return newList;
        }
        
        public List<T> GetAllByInterface<T>() where T : class
        {
            var result = new List<T>();

            foreach (var list in _typedCollections.Values)
            {
                foreach (var item in list)
                {
                    if (item is T tItem)
                    {
                        result.Add(tItem);
                    }
                }
            }

            return result;
        }

        public void Clear()
        {
            _typedCollections.Clear();
        }
    }
}