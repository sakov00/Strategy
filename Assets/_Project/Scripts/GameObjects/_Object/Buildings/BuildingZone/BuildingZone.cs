using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects._Object.BuildingZone
{
    public class BuildingZone : MonoBehaviour, IBuy, ISavableController, IClearData
    {
        [Inject] private AppData _appData;
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private BuildFactory _buildFactory;

        [SerializeField] public BuildingZoneModel _model;
        
        private Vector3 _originalScale;

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
            if (savableModel is BuildingZoneModel buildingZoneModel)
            {
                _model = buildingZoneModel;
            }
        }

        public async UniTask TryBuy()
        {
            if(_originalScale == Vector3.zero)
                _originalScale = transform.localScale;
            
            var reducedScale = _originalScale * 0.9f;
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(_originalScale, 0.25f));
            await sequence.Play();
            
            var buildModel = _buildFactory.GetBuildModel(_model.BuildType);
            if (buildModel.PriceList[0] > _appData.LevelData.Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            _appData.LevelData.Money -= buildModel.PriceList[0];
            var build = _buildFactory.CreateBuild(_model.BuildType, transform.position, transform.rotation);
            build.BuildModel.CurrentLevel++;
            ClearData();
            DestroyObject();
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