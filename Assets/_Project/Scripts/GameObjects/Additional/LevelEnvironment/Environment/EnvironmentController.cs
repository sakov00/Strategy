using System;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
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
        }
        
        public UniTask InitializeAsync()
        {
            _saveRegistry.Register(this);
            return default;
        }

        public ISavableModel GetSavableModel()
        {
            _model.SavePosition = transform.position;
            _model.SaveRotation = transform.rotation;
            return _model;
        }

        public void SetSavableModel(ISavableModel savableModel) =>
            _model.LoadData(savableModel);

        public void Destroy() => Destroy(gameObject);

        private void OnDestroy()
        {
            _saveRegistry.Unregister(this);
        }
    }
}