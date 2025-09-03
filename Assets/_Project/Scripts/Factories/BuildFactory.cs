using System.Collections.Generic;
using System.Linq;
using _General.Scripts.Json;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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

        public BuildController CreateBuild(BuildType buildType, Vector3 position = default, Quaternion rotation = default)
        {
            var prefab = _buildingPrefabConfig.allBuildPrefabs
                .FirstOrDefault(p => p.BuildModel.BuildType == buildType);

            return prefab != null ? _resolver.Instantiate(prefab, position, rotation) : null;
        }

        public BuildModel GetBuildModel(BuildType buildType)
        {
            var prefab = _buildingPrefabConfig.allBuildPrefabs
                .FirstOrDefault(p => p.BuildModel.BuildType == buildType);

            return prefab != null ? prefab.BuildModel : null;
        }
    }
}