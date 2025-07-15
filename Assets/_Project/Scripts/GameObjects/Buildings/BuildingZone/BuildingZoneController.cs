using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using _Project.Scripts.Windows.Presenters;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour, IBuyController, ISavable<BuildingZoneJson>
    {
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private BuildFactory _buildFactory;
        
        [SerializeField] private BuildingZoneModel buildingZoneModel;
        
        private Vector3 _originalScale;

        private void Start()
        {
            InjectManager.Inject(this);
            _saveRegistry.Register(this);
        }
        
        public BuildingZoneJson GetJsonData()
        {
            var buildingZoneJson = new BuildingZoneJson();
            buildingZoneJson.position = transform.position;
            buildingZoneJson.typeBuilding = buildingZoneModel.typeBuilding;
            return buildingZoneJson;
        }

        public void SetJsonData(BuildingZoneJson environmentJson)
        {
            transform.position = environmentJson.position;
            buildingZoneModel.typeBuilding = environmentJson.typeBuilding;
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
            
            var buildModel = _buildFactory.GetBuildModel(buildingZoneModel.typeBuilding);
            if (buildModel.PriceList[0] > AppData.User.Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            AppData.User.Money -= buildModel.PriceList[0];
            _buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, transform.rotation);
            _saveRegistry.Unregister(this);
            Destroy(gameObject);
        }

        public void ClearData()
        {
            Destroy(gameObject);
        }
    }
}