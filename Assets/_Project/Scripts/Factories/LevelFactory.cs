using System.Collections.Generic;
using _General.Scripts.SO;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Environment;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class LevelFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private LevelsConfig _levelsConfig;

        public void CreateLevel(int levelIndex)
        {
            CreateTerrain(levelIndex);
            CreateRoads(levelIndex);
        }
        
        public EnvironmentController CreateEnvironment(EnvironmentType environmentType, Vector3 position, Quaternion rotation = default)
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

        private void CreateTerrain(int levelIndex)
        {
            _resolver.Instantiate(_levelsConfig.terrainControllers[levelIndex]);
        }
        
        private void CreateRoads(int levelIndex)
        {
            foreach (var roadPrefab in _levelsConfig.roadPrefabs[levelIndex].roads)
            {
                _resolver.Instantiate(roadPrefab);
            }
        }
    }
}