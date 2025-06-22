using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Windows.Presenters;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour, IBuyController
    {
        [Inject] private BuildFactory buildFactory;
        [Inject] private GameWindowViewModel gameWindowViewModel;
        
        [SerializeField] private BuildingZoneModel buildingZoneModel;

        private void Awake()
        {
            InjectManager.Inject(this);
        }

        public void TryBuy()
        {
            var buildModel = buildFactory.GetBuildModel(buildingZoneModel.typeBuilding);
            if (buildModel.PriceList[0] > gameWindowViewModel.Money.Value)
            {
                Debug.Log("Not enough money");
                return;
            }
            
            gameWindowViewModel.AddMoney(-buildModel.PriceList[0]);
            
            var createdBuild = 
                buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, Quaternion.identity);
            var newPosition = new Vector3(
                transform.position.x,
                transform.position.y + createdBuild.BuildView.objRenderer.bounds.size.y/2,
                transform.position.z);
            createdBuild.transform.position = newPosition;
            Destroy(gameObject);
        }
    }
}