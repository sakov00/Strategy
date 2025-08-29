using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._General;
using UnityEngine;
using VContainer;

namespace _General.Scripts.Pools
{
    public class ObjectPool
    {
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private Transform _containerTransform;
        
        private readonly List<ObjectController> _availableObjects = new();

        public void SetContainer(Transform transform)
        {
            _containerTransform = transform;
        }
        
        public T Get<T>(Vector3 position = default, Quaternion rotation = default) where T : ObjectController
        {
            var obj = _availableObjects.OfType<T>().FirstOrDefault();

            if (obj != null)
            {
                _availableObjects.Remove(obj);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = _characterFactory.CreateCharacter<T>();
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            _objectsRegistry.Register<T>(obj);
            return obj;
        }

        public void Return<T>(T draggable) where T : ObjectController
        {
            if (!_availableObjects.Contains(draggable))
            {
                _availableObjects.Add(draggable);
            }
            
            draggable.gameObject.SetActive(false);
            draggable.transform.SetParent(_containerTransform, false); 
            _objectsRegistry.Unregister<T>(draggable);
        }
    }
}