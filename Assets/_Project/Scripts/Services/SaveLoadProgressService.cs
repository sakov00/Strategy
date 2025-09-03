using System.Collections.Generic;
using System.IO;
using System.Linq;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using MemoryPack;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Services
{
    public class SaveLoadProgressService
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private EnvironmentFactory _environmentFactory;
        [Inject] private ResetLevelService _resetLevelService;
        
        private static string SavePath => Path.Combine(Application.persistentDataPath, "Level.dat");

        public async UniTask SaveLevel(int index)
        {
            var allObjects = _objectsRegistry.GetAllByInterface<ISavableController>();
            
            var levelModel = new LevelModel();
            levelModel.SavableModels.AddRange(allObjects.Select(o => o.GetSavableModel()).ToList());

            var data = MemoryPackSerializer.Serialize(levelModel);
            await File.WriteAllBytesAsync(SavePath, data);
            Debug.Log($"Level saved to {SavePath}");
        }

        public async UniTask LoadLevel(int index)
        {
            _resetLevelService.ResetLevel();
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("Save file not found!");
                return;
            }

            var data = await File.ReadAllBytesAsync(SavePath);
            LevelModel levelModel = MemoryPackSerializer.Deserialize<LevelModel>(data);

            Debug.Log($"Loaded {levelModel.SavableModels.Count} objects.");

            await InstantiateLoadedObjects(levelModel);
        }

        private async UniTask InstantiateLoadedObjects(LevelModel levelModel)
        {
            foreach (var model in levelModel.SavableModels)
            {

                if (model is BuildModel buildModel)
                {
                    _buildPool.Get(buildModel.BuildType, buildModel.Position, buildModel.Rotation);
                }
                if (model is CharacterModel characterModel)
                {
                    _characterPool.Get(characterModel.CharacterType, characterModel.Position, characterModel.Rotation);
                }
            
                // if (obj.TryGetComponent<IModelReceiver>(out var receiver))
                // {
                //     receiver.LoadModel(model);
                // }
            
                await UniTask.Yield();
            }
        }
    }
}
