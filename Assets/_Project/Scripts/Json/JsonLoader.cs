using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Json
{
    public class JsonLoader
    {
        public void Save<T>(T data, int levelIndex)
        {
            // Сохраняем всегда в persistentDataPath (доступно для записи на всех платформах)
            var fileName = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");
            var json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(fileName, json);
            Debug.Log($"Данные сохранены в {fileName}");
        }

        public async UniTask<T> Load<T>(int levelIndex)
        {
            string fileName = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");

            // Если файл уже есть в persistentDataPath — читаем оттуда
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                Debug.Log("Данные загружены из persistentDataPath");
                return JsonUtility.FromJson<T>(json);
            }

            // Если нет — пробуем загрузить из StreamingAssets (Android требует UnityWebRequest)
            string streamingPath = Path.Combine(Application.streamingAssetsPath, $"LevelData_{levelIndex}.json");

#if UNITY_ANDROID && !UNITY_EDITOR
            using var request = UnityWebRequest.Get(streamingPath);
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Файл не найден: " + streamingPath);
                return default;
            }

            var jsonFromStream = request.downloadHandler.text;
            Debug.Log("Данные загружены из StreamingAssets");
            return JsonUtility.FromJson<T>(jsonFromStream);
#else
            if (!File.Exists(streamingPath))
            {
                Debug.LogWarning("Файл не найден: " + streamingPath);
                return default;
            }

            var jsonFromStream = File.ReadAllText(streamingPath);
            Debug.Log("Данные загружены из StreamingAssets");
            return JsonUtility.FromJson<T>(jsonFromStream);
#endif
        }
    }
}
