using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.Controller;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour, IBuyController, IJsonSerializable
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private BuildFactory _buildFactory;
        
        [SerializeField] private BuildingZoneModel _buildingZoneModel;
        
        private Vector3 _originalScale;

        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }
        
        public ObjectJson GetJsonData()
        {
            var objectJson = new ObjectJson
            {
                objectType = nameof(BuildingZoneController),
                position = transform.position,
                rotation = transform.rotation
            };
            return objectJson;
        }

        public void SetJsonData(ObjectJson objectJson)
        {
            transform.position = objectJson.position;
            transform.rotation = objectJson.rotation;
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
            
            var buildModel = _buildFactory.GetBuildModel(_buildingZoneModel.BuildType);
            if (buildModel.PriceList[0] > AppData.User.Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            AppData.User.Money -= buildModel.PriceList[0];
            var build = _buildFactory.CreateBuild(_buildingZoneModel.BuildType, transform.position, transform.rotation);
            build.BuildModel.CurrentLevel++;
            _objectsRegistry.Unregister(this);
            Destroy(gameObject);
        }

        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}