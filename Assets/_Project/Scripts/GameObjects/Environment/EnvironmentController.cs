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
        [SerializeField] protected EnvironmentType _environmentType;
        
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private ClearDataRegistry _clearDataRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _saveRegistry.Register(this);
            _clearDataRegistry.Register(this);
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