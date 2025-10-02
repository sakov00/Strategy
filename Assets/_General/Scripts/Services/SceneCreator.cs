using System;
using System.Collections.Generic;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain;
using _Project.Scripts.GameObjects.Concrete.BuildingZone;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _General.Scripts.Services
{
    public class SceneCreator 
    {
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildPool _buildPool;
        [Inject] private UnitPool _unitPool;
        [Inject] private EnvironmentFactory _environmentFactory;
        
        private static readonly Dictionary<Type, int> TypePriority = new()
        {
            { typeof(TerrainModel), 0 },
            { typeof(EnemyRoadModel), 1 },
            { typeof(UnitModel), 2 },
            { typeof(BuildingZoneModel), 3 },
            { typeof(BuildModel), 4 },
        };
        
        public async UniTask InstantiateLoadedObjects(LevelModel levelModel)
        {
            SortSavableModels(levelModel);
            foreach (var model in levelModel.SavableModels)
            {
                ISavableController savableController = model switch
                {
                    BuildModel buildModel => 
                        _buildPool.Get(buildModel.BuildType, buildModel.SavePosition, buildModel.SaveRotation),
                    UnitModel unitModel => 
                        _unitPool.Get(unitModel.UnitType, unitModel.SavePosition, unitModel.SaveRotation),
                    TerrainModel terrainModel => 
                        _environmentFactory.CreateTerrain(0),
                    EnemyRoadModel enemyRoadModel => 
                        _environmentFactory.CreateRoads(enemyRoadModel.SavePosition, enemyRoadModel.SaveRotation),
                    BuildingZoneModel buildingZoneModel => 
                        _othersFactory.CreateBuildingZone(buildingZoneModel.SavePosition, buildingZoneModel.SaveRotation),
                    _ => null
                };

                savableController?.SetSavableModel(model);
                savableController?.Initialize();
                
                await UniTask.NextFrame();
            }
        }
        
        public void SortSavableModels(LevelModel levelModel)
        {
            levelModel.SavableModels.Sort((a, b) =>
            {
                int aPriority = GetPriority(a.GetType());
                int bPriority = GetPriority(b.GetType());
                return aPriority.CompareTo(bPriority);
            });
        }

        private int GetPriority(Type type)
        {
            if (TypePriority.TryGetValue(type, out var priority))
                return priority;

            foreach (var kvp in TypePriority)
            {
                if (kvp.Key.IsAssignableFrom(type))
                    return kvp.Value;
            }

            return int.MaxValue;
        }
    }
}