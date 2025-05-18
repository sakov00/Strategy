using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.MoneyBuild;
using UnityEngine;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    [RequireComponent(typeof(BuildingZoneModel))]
    public class BuildingZoneSystem : MonoBehaviour
    {
        private BuildingZoneModel buildingZoneModel;
        [SerializeField] private MoneyBuildModel buildingPrefab;
        [SerializeField] private MoneyBuildModel moneyBuildModel;

        private void OnValidate()
        {
            buildingZoneModel ??= GetComponent<BuildingZoneModel>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerModel>() == null)
                return;
            
            if (moneyBuildModel == null)
            {
                moneyBuildModel = Instantiate(buildingPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                moneyBuildModel.addMoneyValue++;
            }
        }
    }
}