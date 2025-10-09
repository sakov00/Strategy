using System;
using System.Linq;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Enums;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using _Project.Scripts.SO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Concrete.BuildingZone
{
    public class BuildingZone : MonoBehaviour, IBuy, ISavableController, IDestroyable
    {
        [Inject] private AppData _appData;
        [Inject] private BuildingPrefabConfig _buildingPrefabConfig;
        [Inject] private BuildPool _buildPool;
        [Inject] private ObjectsRegistry _objectsRegistry;

        [SerializeField] public BuildingZoneModel _model;
        
        private Vector3 _originalScale;

        private void Awake()
        {
            InjectManager.Inject(this);
            if (_appData.AppMode == AppMode.Redactor)
                _objectsRegistry.Register(this);
        }

        public void Initialize()
        {
            _objectsRegistry.Register(this);
        }

        public async UniTask TryBuy()
        {
            if (_originalScale == Vector3.zero)
                _originalScale = transform.localScale;

            var reducedScale = _originalScale * 0.9f;

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(_originalScale, 0.25f));
            await sequence.Play();

            var prefab = _buildingPrefabConfig.allBuildPrefabs.First(p => p.BuildType == _model.BuildType);
            var price = prefab.BuildPrice;
            if (price > _appData.LevelData.LevelMoney)
            {
                Debug.Log("Not enough money");
                return;
            }

            _appData.LevelData.LevelMoney -= price;
            var build = _buildPool.Get(_model.BuildType, transform.position, transform.rotation);
            build.Initialize();
            Destroy();
        }

        public ISavableModel GetSavableModel()
        {
            _model.SavePosition = transform.position;
            _model.SaveRotation = transform.rotation;
            return _model;
        }

        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is BuildingZoneModel buildingZoneModel) _model = buildingZoneModel;
        }

        public void Destroy() => Destroy(gameObject);

        private void OnDestroy()
        {
            _objectsRegistry.Unregister(this);
        }
    }
}