using _General.Scripts._VContainer;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Environment
{
    public class EnvironmentController : MonoBehaviour, IJsonSerializable
    {
        [SerializeField] protected EnvironmentType _environmentType;
        
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }

        public ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(EnvironmentController),
                position = transform.position,
                rotation = transform.rotation
            };
            return objectJson;
        }

        public void SetJsonData(ObjectJson objectJson)
        {
            transform.position = objectJson.position;
            transform.rotation = objectJson.rotation;
        }
        
        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}