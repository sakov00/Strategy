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

        public void CreateBuild()
        {
            var createdBuild = 
                buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, Quaternion.identity);
            var newPosition = new Vector3(
                transform.position.x,
                transform.position.y + createdBuild.View.objRenderer.bounds.size.y/2,
                transform.position.z);
            createdBuild.transform.position = newPosition;
            Destroy(gameObject);
        }
        
        private void OnTriggerExit(Collider other)
        {

        }
    }
}