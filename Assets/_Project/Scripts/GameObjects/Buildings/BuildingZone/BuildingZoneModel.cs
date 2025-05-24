using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.BuildingZone
{
    public class BuildingZoneModel : MonoBehaviour
    {
        [SerializeField] public TypeBuilding typeBuilding;
        [SerializeField] public IUpgradable upgradableObject;
    }
}