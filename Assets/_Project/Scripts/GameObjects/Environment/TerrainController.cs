using _General.Scripts._VContainer;
using _General.Scripts.Registries;
using _Project.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Environment
{
    public class TerrainController : MonoBehaviour, IClearData
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }
        
        public virtual void ClearData()
        {
            Destroy(gameObject);
        }
    }
}