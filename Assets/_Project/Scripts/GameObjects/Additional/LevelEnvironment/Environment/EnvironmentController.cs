using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Additional.LevelEnvironment.Environment
{
    public class EnvironmentController : MonoBehaviour, ISavableController, IClearScene
    {
        [SerializeField] protected EnvironmentModel _model;
        [Inject] private ObjectsRegistry _objectsRegistry;

        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }

        private void OnDestroy()
        {
            ClearData();
        }

        public void ClearData()
        {
            _objectsRegistry.Unregister(this);
        }

        public void DeleteFromScene(bool realDelete = false)
        {
            Destroy(gameObject);
        }

        public ISavableModel GetSavableModel()
        {
            _model.SavePosition = transform.position;
            _model.SaveRotation = transform.rotation;
            return _model;
        }

        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is EnvironmentModel buildingZoneModel) _model = buildingZoneModel;
        }
    }
}