using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Networking;
#endif

namespace _General.Scripts.Json
{
    public class JsonLoader
    {
        // Сохраняем данные в persistentDataPath
        public async UniTask Save<T>(T data, int levelIndex)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"LevelData_{levelIndex}.json");

            // Сериализация через Newtonsoft с красивым форматированием
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, 
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            await File.WriteAllTextAsync(filePath, json);
            Debug.Log($"Данные сохранены в {filePath}");
        }

        // Загружаем данные
        public async UniTask<T> Load<T>(int levelIndex)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"LevelData_{levelIndex}.json");

            // Читаем из persistentDataPath, если файл существует
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                Debug.Log("Данные загружены из persistentDataPath");
                return JsonConvert.DeserializeObject<T>(json, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            }

            // Если нет — читаем из StreamingAssets
            string streamingPath = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");

#if UNITY_ANDROID && !UNITY_EDITOR
            using var request = UnityWebRequest.Get(streamingPath);
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Файл не найден: " + streamingPath);
                return default;
            }

            string jsonFromStream = request.downloadHandler.text;
            Debug.Log("Данные загружены из StreamingAssets");
            return JsonConvert.DeserializeObject<T>(jsonFromStream, 
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
#else
            if (!File.Exists(streamingPath))
            {
                Debug.LogWarning("Файл не найден: " + streamingPath);
                return default;
            }

            string jsonFromStream = await File.ReadAllTextAsync(streamingPath);
            Debug.Log("Данные загружены из StreamingAssets");
            return JsonConvert.DeserializeObject<T>(jsonFromStream,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
#endif
        }
    }
}
