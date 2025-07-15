using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Environment
{
    public class EnvironmentController : MonoBehaviour, ISavable<EnvironmentJson>
    {
        [SerializeField] protected EnvironmentType environmentType;
        
        [Inject] private SaveRegistry _saveRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _saveRegistry.Register(this);
        }

        public virtual EnvironmentJson GetJsonData()
        {
            var playerJson = new EnvironmentJson
            {
                position = transform.position,
                rotation = transform.rotation,
                environmentType = environmentType
            };
            return playerJson;
        }

        public virtual void SetJsonData(EnvironmentJson environmentJson)
        {
            transform.position = environmentJson.position;
            transform.rotation = environmentJson.rotation;
            environmentType = environmentJson.environmentType;
        }
        
        public virtual void ClearData()
        {
            Destroy(gameObject);
        }
    }
}