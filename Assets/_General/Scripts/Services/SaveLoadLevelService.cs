using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Concrete.ArcherFriend;
using _Project.Scripts.GameObjects.Concrete.WarriorFriend;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using MemoryPack;
using UnityEngine;
using VContainer;
using K4os.Compression.LZ4;
using BuildingZoneModel = _Project.Scripts.GameObjects.Concrete.BuildingZone.BuildingZoneModel;
using BuildModel = _Project.Scripts.GameObjects.Abstract.BuildModel;
using EnemyRoadModel = _Project.Scripts.GameObjects.Additional.EnemyRoads.EnemyRoadModel;
using TerrainModel = _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain.TerrainModel;
using UnitModel = _Project.Scripts.GameObjects.Abstract.Unit.UnitModel;

namespace _General.Scripts.Services
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
        
        private static string GetDefaultSavePath(int index) 
            => Path.Combine(Application.streamingAssetsPath, $"level_{index}.dat");
        private static string GetProgressSavePath(int index) 
            => Path.Combine(Application.persistentDataPath, $"level_progress_{index}.dat");

        public async UniTask SaveLevelDefault(int index) => await Save(GetDefaultSavePath(index));
        public async UniTask LoadLevelDefault(int index) => await Load(GetDefaultSavePath(index));
        public async UniTask SaveLevelProgress(int index) => await Save(GetProgressSavePath(index));
        public async UniTask LoadLevelProgress(int index) => await Load(GetProgressSavePath(index));

        public async UniTask LoadLevel(int index)
        {
            if (File.Exists(GetProgressSavePath(index)))
                await LoadLevelDefault(index);
            else
                await LoadLevelProgress(index);
        }

        public void RemoveProgress(int index)
        {
            if(File.Exists(GetProgressSavePath(index)))
                File.Delete(GetProgressSavePath(index));
        }

        private async UniTask Save(string path)
        {
            var allObjects = _objectsRegistry.GetAllByInterface<ISavableController>();
            var filteredAllObjects = 
                allObjects.Where(x => x is not ArcherFriendController && x is not WarriorFriendController).ToList();
            
            var levelModel = new LevelModel();
            levelModel.SavableModels.AddRange(filteredAllObjects.Select(o => o.GetSavableModel()).ToList());

            var data = MemoryPackSerializer.Serialize(levelModel);
            var compressed = LZ4Pickler.Pickle(data);
            await File.WriteAllBytesAsync(path, compressed);
            
            Debug.Log($"Level saved to {path}");
        }

        private async UniTask Load(string path)
        {
            _resetLevelService.ResetLevel();
            if (!File.Exists(path))
            {
                Debug.LogWarning("Save file not found!");
                return;
            }

            var compressed = await File.ReadAllBytesAsync(path);
            var data = LZ4Pickler.Unpickle(compressed);
            LevelModel levelModel = MemoryPackSerializer.Deserialize<LevelModel>(data);

            Debug.Log($"Loaded {levelModel.SavableModels.Count} objects.");

            await InstantiateLoadedObjects(levelModel);
        }

        private async UniTask InstantiateLoadedObjects(LevelModel levelModel)
        {
            foreach (var model in levelModel.SavableModels)
            {
                ISavableController savableController = model switch
                {
                    BuildModel buildModel => 
                        _buildPool.Get(buildModel.BuildType, buildModel.SavePosition, buildModel.SaveRotation),
                    UnitModel characterModel => 
                        _characterPool.Get(characterModel.UnitType, characterModel.SavePosition, characterModel.SaveRotation),
                    TerrainModel terrainModel => 
                        _environmentFactory.CreateTerrain(0),
                    EnemyRoadModel enemyRoadModel => 
                        _environmentFactory.CreateRoads(enemyRoadModel.SavePosition, enemyRoadModel.SaveRotation),
                    BuildingZoneModel buildingZoneModel => 
                        _othersFactory.CreateBuildingZone(buildingZoneModel.SavePosition, buildingZoneModel.SaveRotation),
                    _ => null
                };

                savableController?.SetSavableModel(model);

                await UniTask.Yield();
            }
        }
    }
}
