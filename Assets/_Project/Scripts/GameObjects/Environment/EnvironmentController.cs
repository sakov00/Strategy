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
    public class EnvironmentController : MonoBehaviour, ISavable<EnvironmentJson>
    {
        [SerializeField] protected EnvironmentType _environmentType;
        
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }

        public virtual EnvironmentJson GetJsonData()
        {
            var playerJson = new EnvironmentJson
            {
                position = transform.position,
                rotation = transform.rotation,
                environmentType = _environmentType
            };
            return playerJson;
        }

        public virtual void SetJsonData(EnvironmentJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            _environmentType = environmentJson.environmentType;
        }
        
        public virtual void ClearData()
        {
            Destroy(gameObject);
        }
    }
}