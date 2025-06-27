using System.IO;
using UnityEngine;

namespace _Project.Scripts.Json
{
    public class JsonLoader<T>
    {
        private readonly string _fileName;
        
        public JsonLoader()
        {
            _fileName = Path.Combine(Application.streamingAssetsPath, "fileName");
        }
        
        public void Save(T data)
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(_fileName, json);
            Debug.Log($"Данные сохранены в {_fileName}");
        }

        public T Load()
        {
            if (!File.Exists(_fileName))
            {
                Debug.LogWarning("Файл не найден: " + _fileName);
                return default;
            }

            string json = File.ReadAllText(_fileName);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("Данные загружены");
            return data;
        }
    }
}