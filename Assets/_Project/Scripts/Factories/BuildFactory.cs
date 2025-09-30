using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using BuildModel = _Project.Scripts.GameObjects.Abstract.BuildModel;

namespace _Project.Scripts.Factories
{
    public class BuildFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private BuildingPrefabConfig _buildingPrefabConfig;

        public T CreateBuild<T>(Vector3 position = default, Quaternion rotation = default) where T : BuildController
        {
            foreach (var prefab in _buildingPrefabConfig.allBuildPrefabs)
            {
                if (prefab is T tPrefab)
                {
                    return _resolver.Instantiate(tPrefab, position, rotation);
                }
            }
            return null;
        }
    }
}