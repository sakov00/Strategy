using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _General.Scripts.AllAppData;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract;
using Cysharp.Threading.Tasks;
using MemoryPack;
using UnityEngine;
using VContainer;
using K4os.Compression.LZ4;

namespace _General.Scripts.Services
{
    public class SaveLoadLevelService
    {
        [Inject] private AppData _appData;
        [Inject] private SaveRegistry _saveRegistry;
        
        private Dictionary<BuildType, Func<BuildController>> _buildTypeToClass;
        
        private static string GetDefaultSavePath(int index) 
            => Path.Combine(Application.streamingAssetsPath, $"level_{index}.dat");
        private static string GetProgressSavePath(int index) 
            => Path.Combine(Application.persistentDataPath, $"level_progress_{index}.dat");

        public async UniTask LoadLevelDefault(int index) => await Load(GetDefaultSavePath(index));
        public async UniTask LoadLevelProgress(int index) => await Load(GetProgressSavePath(index));
        public async UniTask SaveLevelDefault(int index) => await Save(GetDefaultSavePath(index));
        public async UniTask SaveLevelProgress(int index) => await Save(GetProgressSavePath(index));

        public async UniTask LoadLevel(int index)
        {
            if (File.Exists(GetProgressSavePath(index)))
                await LoadLevelProgress(index);
            else
                await LoadLevelDefault(index);
        }

        public void RemoveProgress(int index)
        {
            if(File.Exists(GetProgressSavePath(index)))
                File.Delete(GetProgressSavePath(index));
        }

        private async UniTask Save(string path)
        {
            var allObjects = _saveRegistry.GetAll();
            _appData.LevelData.SavableModels = allObjects.Select(o => o.GetSavableModel()).ToList();

            var data = MemoryPackSerializer.Serialize(_appData.LevelData);
            var compressed = LZ4Pickler.Pickle(data);
            await File.WriteAllBytesAsync(path, compressed);
            
            Debug.Log($"Level saved to {path}");
        }

        private async UniTask Load(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning("Save file not found!");
                return;
            }

            var compressed = await File.ReadAllBytesAsync(path);
            var data = LZ4Pickler.Unpickle(compressed);
            await UniTask.NextFrame();
            var levelData = MemoryPackSerializer.Deserialize<LevelData>(data);
            await UniTask.NextFrame();
            _appData.LevelData.SetData(levelData);
        }
    }
}
