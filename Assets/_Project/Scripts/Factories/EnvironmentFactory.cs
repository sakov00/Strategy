using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Environment;
using _Project.Scripts.Json;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class EnvironmentFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private EnvironmentPrefabConfig _environmentPrefabConfig;

        public List<EnvironmentController> CreateEnvironments(int levelIndex, List<EnvironmentJson> environmentJsons)
        {
            var environmentControllers = new List<EnvironmentController>();
            foreach (var environmentJson in environmentJsons)
            {
                EnvironmentController environmentController = null;
                if (environmentJson.environmentType == EnvironmentType.Terrain)
                    environmentController = CreateTerrain(levelIndex, environmentJson.position, environmentJson.rotation);
                else
                    environmentController = CreateEnvironment(environmentJson.environmentType, environmentJson.position, environmentJson.rotation);
                
                environmentControllers.Add(environmentController);
            }

            return environmentControllers;
        }
        
        private EnvironmentController CreateEnvironment(EnvironmentType environmentType, Vector3 position, Quaternion rotation = default)
        {
            EnvironmentController environmentController = null;
            switch (environmentType)
            {
                case EnvironmentType.Tree:
                    environmentController = null;
                    break;
            }
            return environmentController;
        }

        private TerrainController CreateTerrain(int levelIndex, Vector3 position = default, Quaternion rotation = default)
        {
            return _resolver.Instantiate(_environmentPrefabConfig.terrainControllers[levelIndex], position, rotation);
        }
    }
}