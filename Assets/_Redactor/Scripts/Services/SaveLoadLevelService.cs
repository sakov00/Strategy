using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects._Object.BuildingZone;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.LevelEnvironment.Terrain;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using MemoryPack;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts.Services
{
    public class SaveLoadLevelService
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private EnvironmentFactory _environmentFactory;
        [Inject] private ResetLevelService _resetLevelService;
        
        private Dictionary<BuildType, Func<BuildController>> _buildTypeToClass;
        
        private static string GetSavePath(int index) 
            => Path.Combine(Application.persistentDataPath, $"level_{index}.dat");

        public async UniTask SaveLevel(int index)
        {
            var allObjects = _objectsRegistry.GetAllByInterface<ISavableController>();
            var levelModel = new LevelModel();
            levelModel.SavableModels.AddRange(allObjects.Select(o => o.GetSavableModel()).ToList());

            var data = MemoryPackSerializer.Serialize(levelModel);
            await File.WriteAllBytesAsync(GetSavePath(index), data);
            Debug.Log($"Level saved to {GetSavePath(index)}");
        }

        public async UniTask LoadLevel(int index)
        {
            _resetLevelService.ResetLevel();
            if (!File.Exists(GetSavePath(index)))
            {
                Debug.LogWarning("Save file not found!");
                return;
            }

            var data = await File.ReadAllBytesAsync(GetSavePath(index));
            LevelModel levelModel = MemoryPackSerializer.Deserialize<LevelModel>(data);

            Debug.Log($"Loaded {levelModel.SavableModels.Count} objects.");

            await InstantiateLoadedObjects(levelModel, index);
        }

        private async UniTask InstantiateLoadedObjects(LevelModel levelModel, int levelIndex)
        {
            foreach (var model in levelModel.SavableModels)
            {
                ISavableController savableController = null;
                if (model is BuildModel buildModel)
                {
                    savableController = _buildPool.Get(buildModel.BuildType, buildModel.Position, buildModel.Rotation);
                }
                if (model is CharacterModel characterModel)
                {
                    savableController = _characterPool.Get(characterModel.CharacterType, characterModel.Position, characterModel.Rotation);
                }
                if (model is TerrainModel terrainModel)
                {
                    savableController = _environmentFactory.CreateTerrain(levelIndex);
                }
                // if (model is EnvironmentModel environmentModel)
                // {
                //     _environmentFactory.CreateEnvironment(levelIndex);
                // }
                if (model is EnemyRoadModel enemyRoadModel)
                {
                    savableController = _environmentFactory.CreateRoads(enemyRoadModel.Position, enemyRoadModel.Rotation);
                }
                if (model is BuildingZoneModel bindingZoneModel)
                {
                    savableController = _othersFactory.CreateBuildingZone(bindingZoneModel.Position, bindingZoneModel.Rotation);
                }
                savableController?.SetSavableModel(model);
                await UniTask.Yield();
            }
        }
    }
}
