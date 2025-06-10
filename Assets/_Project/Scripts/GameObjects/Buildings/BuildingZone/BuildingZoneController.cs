using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts._VContainer;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Player;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneController : MonoBehaviour
    {
        [Inject] private BuildFactory buildFactory;
        
        [SerializeField] private BuildingZoneModel buildingZoneModel;

        private void Awake()
        {
            InjectManager.Inject(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerModel>() == null)
                return;
            
            if (buildingZoneModel.createdBuild == null)
            {
                buildingZoneModel.createdBuild = 
                    buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, Quaternion.identity);
                var newPosition = new Vector3(
                    transform.position.x,
                    buildingZoneModel.createdBuild.objRenderer.bounds.size.y/2,
                    transform.position.z);
                buildingZoneModel.createdBuild.transform.position = newPosition;
            }
            else
            {
                buildingZoneModel.createdBuild.currentLevel++;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {

        }
    }
}