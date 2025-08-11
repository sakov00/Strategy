using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Environment
{
    public class TerrainController : MonoBehaviour, IClearData
    {
        [Inject] private ClearDataRegistry _clearDataRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _clearDataRegistry.Register(this);
        }
        
        public virtual void ClearData()
        {
            Destroy(gameObject);
        }
    }
}