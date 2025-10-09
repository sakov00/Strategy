using System;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Additional.LevelEnvironment.Environment
{
    public class EnvironmentController : MonoBehaviour, ISavableController, IDestroyable
    {
        [Inject] private AppData _appData;
        [Inject] private SaveRegistry _saveRegistry;
        [SerializeField] protected EnvironmentModel _model;

        private void Awake()
        {
            InjectManager.Inject(this);
            if(_appData.AppMode == AppMode.Redactor)
                _saveRegistry.Register(this);
        }
        
        public void Initialize()
        {
            _saveRegistry.Register(this);
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

        public void Destroy() => Destroy(gameObject);

        private void OnDestroy()
        {
            _saveRegistry.Unregister(this);
        }
    }
}