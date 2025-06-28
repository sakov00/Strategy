using System.IO;
using UnityEngine;

namespace _Project.Scripts.Json
{
    public class JsonLoader
    {
        public void Save<T>(T data, int levelIndex)
        {
            var fileName = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");
            var json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(fileName, json);
            Debug.Log($"Данные сохранены в {fileName}");
        }

        public T Load<T>(int levelIndex)
        {
            var fileName = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");
            if (!File.Exists(fileName))
            {
                Debug.LogWarning("Файл не найден: " + fileName);
                return default;
            }

            var json = File.ReadAllText(fileName);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("Данные загружены");
            return data;
        }
    }
}