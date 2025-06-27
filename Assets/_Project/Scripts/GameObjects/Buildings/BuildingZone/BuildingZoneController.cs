using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Registries;
using _Project.Scripts.Services;
using _Project.Scripts.Windows.Presenters;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour, IBuyController
    {
        [Inject] private BuildingZoneRegistry _buildingZoneRegistry;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private GameWindowViewModel _gameWindowViewModel;
        
        [SerializeField] private BuildingZoneModel buildingZoneModel;
        
        private Vector3 _originalScale;

        private void Start()
        {
            InjectManager.Inject(this);
            _buildingZoneRegistry.Register(buildingZoneModel);
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
            if (buildModel.PriceList[0] > _gameWindowViewModel.Money.Value)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            _gameWindowViewModel.AddMoney(-buildModel.PriceList[0]);
            _buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}