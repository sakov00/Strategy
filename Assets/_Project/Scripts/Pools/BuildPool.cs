using System.Collections.Generic; 
using System.Linq;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects._Object;
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
        
        public List<BuildController> GetAvailableBuilds() => _availableBuilds;
        
        public BuildController Get(BuildType buildType, Vector3 position = default, Quaternion rotation = default) 
        {
            var build = _availableBuilds.FirstOrDefault(c => c.BuildModel.BuildType == buildType);
            if (build != null)
            {
                _availableBuilds.Remove(build);
                build.transform.position = position;
                build.transform.rotation = rotation;
                build.gameObject.SetActive(true);
                build.Initialize();
            }
            else
            {
                build = _buildFactory.CreateBuild(buildType, position, rotation);
            }

            build.transform.SetParent(null);
            return build;
        }
        
        public T Get<T>(Vector3 position = default, Quaternion rotation = default) where T : BuildController
        {
            var build = _availableBuilds.OfType<T>().FirstOrDefault();

            if (build != null)
            {
                _availableBuilds.Remove(build);
                build.transform.position = position;
                build.transform.rotation = rotation;
                build.gameObject.SetActive(true);
            }
            else
            {
                build = _buildFactory.CreateBuild<T>(position, rotation);
            }
            
            build.transform.SetParent(null);
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
        
        public void Remove<T>(T build) where T : BuildController
        {
            if (!_availableBuilds.Contains(build))
            {
                _availableBuilds.Remove(build);
            }
        }
    }
}