using System.Collections.Generic; 
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Pools
{
    public class BuildPool
    {
        [Inject] private BuildFactory _buildFactory;
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private Transform _containerTransform;
        private readonly List<BuildController> _availableBuilds = new();

        public void SetContainer(Transform transform)
        {
            _containerTransform = transform;
        }
        
        public T Get<T>(Vector3 position = default, Quaternion rotation = default) where T : BuildController
        {
            var build = _availableBuilds.OfType<T>().FirstOrDefault();

            if (build != null)
            {
                _availableBuilds.Remove(build);
                build.gameObject.SetActive(true);
            }
            else
            {
                build = _buildFactory.CreateBuild<T>(position, rotation);
            }
            
            _objectsRegistry.Register(build);
            return build;
        }

        public void Return<T>(T build) where T : BuildController
        {
            if (!_availableBuilds.Contains(build))
            {
                _availableBuilds.Add(build);
            }
            
            build.gameObject.SetActive(false);
            build.transform.SetParent(_containerTransform, false); 
            _objectsRegistry.Unregister(build);
        }
    }
}