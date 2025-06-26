using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Windows.Presenters;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour, IBuyController
    {
        [Inject] private BuildFactory buildFactory;
        [Inject] private GameWindowViewModel gameWindowViewModel;
        
        [SerializeField] private BuildingZoneModel buildingZoneModel;
        
        private Vector3 originalScale;

        private void Awake()
        {
            InjectManager.Inject(this);
        }

        public async UniTask TryBuy()
        {
            if(originalScale == Vector3.zero)
                originalScale = transform.localScale;
            
            var reducedScale = originalScale * 0.9f;
            
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(reducedScale, 0.25f));
            sequence.Append(transform.DOScale(originalScale, 0.25f));
            await sequence.Play();
            
            var buildModel = buildFactory.GetBuildModel(buildingZoneModel.typeBuilding);
            if (buildModel.PriceList[0] > gameWindowViewModel.Money.Value)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            gameWindowViewModel.AddMoney(-buildModel.PriceList[0]);
            buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}