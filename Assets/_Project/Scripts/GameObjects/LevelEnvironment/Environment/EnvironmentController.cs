using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.LevelEnvironment.Environment
{
    public class EnvironmentController : MonoBehaviour, ISavableController, IClearData
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] protected EnvironmentModel _model;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }

        public ISavableModel GetSavableModel()
        {
            _model.Position = transform.position;
            _model.Rotation = transform.rotation;
            return _model;
        }
        
        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is EnvironmentModel buildingZoneModel)
            {
                _model = buildingZoneModel;
            }
        }
        
        private void OnDestroy()
        {
            ClearData();
        }
        
        public void ClearData()
        {
            _objectsRegistry.Unregister(this);
        }
        
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}