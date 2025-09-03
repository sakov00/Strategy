using System.Collections.Generic;
using _General.Scripts.SO;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.LevelEnvironment.Environment;
using _Project.Scripts.GameObjects.LevelEnvironment.Terrain;
using _Project.Scripts.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factories
{
    public class EnvironmentFactory
    {
        [Inject] private IObjectResolver _resolver;
        [Inject] private LevelsConfig _levelsConfig;
        
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

        public TerrainController CreateTerrain(int levelIndex)
        {
            return _resolver.Instantiate(_levelsConfig.terrainControllers[levelIndex]);
        }
        
        public void CreateRoads(int levelIndex)
        {
            foreach (var roadPrefab in _levelsConfig.roadPrefabs[levelIndex].roads)
            {
                _resolver.Instantiate(roadPrefab);
            }
        }
    }
}