using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.MoneyBuild;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    [RequireComponent(typeof(BuildingZoneModel))]
    public class BuildingZoneSystem : MonoBehaviour
    {
        [Inject] private BuildFactory buildFactory;
        
        [SerializeField, HideInInspector] private BuildingZoneModel buildingZoneModel;

        private void OnValidate()
        {
            buildingZoneModel ??= GetComponent<BuildingZoneModel>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerModel>() == null)
                return;
            
            if (buildingZoneModel.buildModel == null)
            {
                buildingZoneModel.buildModel = 
                    buildFactory.CreateBuild(buildingZoneModel.typeBuilding, transform.position, Quaternion.identity);
                var newPosition = new Vector3(
                    transform.position.x,
                    buildingZoneModel.buildModel.objRenderer.bounds.size.y/2,
                    transform.position.z);
                buildingZoneModel.buildModel.transform.position = newPosition;
            }
            else
            {
                buildingZoneModel.buildModel.CurrentLevel++;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {

        }
    }
}