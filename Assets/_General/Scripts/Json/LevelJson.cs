using System;
using System.Collections.Generic;
using UnityEngine;

namespace _General.Scripts.Json
{
    [Serializable]
    public class LevelJson
    {
        // Все объекты уровня (постройки, игроки, юниты, окружение) в одном списке
        public List<ObjectJson> objects = new();
    }
    
    [Serializable]
    public class ObjectJson
    {
        public string objectType;
        public Vector3 position;
        public Quaternion rotation;
        public string extraDataJson;
    }
}