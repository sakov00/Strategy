using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Registries
{
    public class BuildRegistry
    {
        private readonly Dictionary<Type, IList> _typedCollections = new();

        public void Register<T>(T controller) where T : BuildController
        {
            var type = typeof(T);
            if (!_typedCollections.TryGetValue(type, out var list))
            {
                list = new List<T>();
                _typedCollections[type] = list;
            }

            ((List<T>)list).Add(controller);
        }

        public void Unregister<T>(T controller) where T : BuildController
        {
            var type = typeof(T);
            if (_typedCollections.TryGetValue(type, out var list))
            {
                ((List<T>)list).Remove(controller);
            }
        }

        public List<T> GetAll<T>() where T : BuildController
        {
            var type = typeof(T);
            if (_typedCollections.TryGetValue(type, out var list))
            {
                return (List<T>)list;
            }
            
            var newList = new List<T>();
            _typedCollections[type] = newList;
            return newList;
        }

        public void Clear()
        {
            _typedCollections.Clear();
        }
    }
}